import json
import sys

def process_move(gameboard, moves_requested):
    """
    Given a gameboard graph and list of moves (both in JSON format), process the moves and return a tuple
    containing the next game state, list of moves processed, and list of moves with errors (i.e. not processed).

    :param gameboard:
    :param moves:
    :return:
       tuple of gameboard, moves_processed, moves_with_errors
    """

    result_board = json.loads(gameboard)
    # TODO: validate gameboard state

    # note: moves are validated in the loops below
    moves = json.loads(moves_requested)

    result_moves_processed = {'moves': []}
    result_moves_with_errors = {'moves': []}

    node_dict = {}

    # Coerce int properties to be ints:
    for node in result_board['nodes']:
        node['id'] = int(node['id'])
        node['points'] = int(node['points'])
        node_dict[node['id']] = node

    for move in moves['moves']:
        try:
            move['points'] = int(move['points'])
            move['node_from'] = int(move['node_from'])
            move['node_to'] = int(move['node_to'])
            result_moves_processed['moves'].append(move)
        except Exception:
            result_moves_with_errors['moves'].append(move)

    iterable_moves_list = result_moves_processed['moves'][:]
    result_moves_processed = {'moves': []}
    for move in iterable_moves_list:
        # print "processing move..."
        # print "node_from : {0}".format(move['node_from'])
        # print "node_to : {0}".format(move['node_to'])
        # print "owner : {0}".format(move['owner'])
        # print "points : {0}".format(move['points'])

        try:
            from_node = node_dict[move['node_from']]
            to_node = node_dict[move['node_to']]
        except Exception:
            result_moves_with_errors['moves'].append(move)
            continue

        # First pass does two things:
        # - Does all checks, fails invalid moves by putting them into result_moves_with_errors
        # - For all valid moves, the points moving are removed from the FROM node.
        #   These moves are stored in result_moves_processed.
        #
        # Second pass (through the result_moves_processed) is where the bloodbath ensues, and
        # final TO node owner/points are calculated and assigned.
        #
        # This first pass just sets the stage (puts the valid moves' points in motion)
        # path is ok
        if from_node['owner'] == move['owner']:
            # owner is right
            points_remaining = from_node['points'] - move['points']
            if points_remaining >= 0:
                from_node['points'] = points_remaining
                result_moves_processed['moves'].append(move)
            else:
                # not enough points in the from_node
                result_moves_with_errors['moves'].append(move)
        else:
            # wrong owner
            result_moves_with_errors['moves'].append(move)

    # Second pass determines winner of each node, sets points and owner accordingly:
    # dictionary of node_id -> dict(owner, points)
    node_fight_dict = {}

    # Collect all moving armies to the fight for the destination node
    for move in result_moves_processed['moves']:
        player_move = {'owner': move['owner'], 'points': move['points']}
        key = move['node_to']
        if node_fight_dict.has_key(key):
            node_fight_dict[key].append(player_move)
        else:
            node_fight_dict[key] = [player_move]

    # Add current owners to the fights:
    for destination_id in node_fight_dict.keys():
        node_fight_dict[destination_id].append(
            {'owner': node_dict[destination_id]['owner'], 'points': node_dict[destination_id]['points']})

    # Determine winners:
    for node_id in node_fight_dict.keys():
        top_owner = None
        top_points = 0
        for test_move in node_fight_dict[node_id]:
            if test_move['points'] > top_points:
                top_points = test_move['points']
                top_owner = test_move['owner']
            # else:
            #     print "missed"


        # need to find minimum value that's >= 0 --- that's the largest army minus the second largest (in points)
        leftover_points = 0
        for test_move in node_fight_dict[node_id]:
            test_points = top_points - test_move['points']
            if test_points >= leftover_points:
                leftover_points = test_points
            # else:
            #     print "missed"

        # Update destination node attributes:
        # Only change owner if someone is left standing.  If zeroed out, original owner retains control of the node:
        if leftover_points > 0:
            node_dict[node_id]['owner'] = top_owner
        # else:
        #     print "missed"


        node_dict[node_id]['points'] = leftover_points

        # print 'node_id: {0}\npoints: {1}\nowner: {2}'.format(
        #     node_dict[node_id], node_dict[node_id]['points'], node_dict[node_id]['owner'])
        # sys.stdout.flush()

    # Everyone with a dog in the fight is known; reduce and apply final values to nodes:
    return result_board, result_moves_processed, result_moves_with_errors
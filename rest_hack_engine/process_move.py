import json


def process_move(gameboard, moves_requested):
    """
    Given a gameboard graph and list of moves (both in JSON format), process the moves and return a tuple
    containing the next game state, list of moves processed, and list of moves with errors (i.e. not processed).

    :param gameboard:
    :param moves:
    :return:
       tuple of gameboard, moves_processed, moves_with_errors
    """

    board = json.loads(gameboard)
    # TODO: validate gameboard state

    # note: moves are validated in the loops below
    moves = json.loads(moves_requested)

    result_board = {'nodes': ()}
    result_moves_processed   = {'moves': ()}
    result_moves_with_errors = {'moves': ()}

    for move in moves['moves']:
        print "processing move..."
        print "node_from : {0}".format(move['node_from'])
        print "node_to : {0}".format(move['node_to'])
        print "owner : {0}".format(move['owner'])
        print "points : {0}".format(move['points'])

        node_from_id = move['node_from']
        node_to_id = move['node_to']

        from_node = None
        to_node = None
        for node in board['nodes']:
            if node['id'] == node_from_id:
                from_node = node

            if node['id'] == node_to_id:
                to_node = node

        if from_node and to_node:
            print "move from-to path OK"
            result_moves_processed['moves'].add(move)
        else:
            print "invalid path: {0}-{1}".format(node_from_id, node_to_id)
            result_moves_with_errors['moves'].add(move)

    # TODO: actually process the moves and make a result_board

    return result_board, result_moves_processed, result_moves_with_errors
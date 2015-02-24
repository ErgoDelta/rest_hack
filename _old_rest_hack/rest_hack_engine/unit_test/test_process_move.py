import unittest
import process_move as engine

class TestRestHackEngine(unittest.TestCase):

    def setUp(self):
        """
        """

    def tearDown(self):
        """
        """

    def test_check_empty_board_no_moves(self):
        """
        """
        board, moves_processed, moves_with_errors = engine.process_move('{"nodes" : []}', '{"moves" : []}')
        self.assertEqual(board, {'nodes': []})
        self.assertEqual(moves_processed, {'moves': []})
        self.assertEqual(moves_with_errors, {'moves': []})

    def test_check_nonempty_board_no_moves(self):
        """
        """
        board, moves_processed, moves_with_errors = engine.process_move(
            '''{"nodes" : [
                {
                    "id" : 1,
                    "owner" : "player_1",
                    "points" : 100,
                    "connections" : [5],
                    "starting_node" : "player_1"
                }
            ]}''', '{"moves" : []}')

        self.assertDictEqual(board, {
            u'nodes': [
                {
                    u"id": 1,
                    u"owner": u"player_1",
                    u"points": 100,
                    u"connections": [5],
                    u"starting_node": u"player_1"
                }
            ]
        })

        self.assertDictEqual(moves_processed, {'moves': []})
        self.assertDictEqual(moves_with_errors, {'moves': []})


    def test_check_one_invalid_move_missing_node_from(self):
        """
        """
        board, moves_processed, moves_with_errors = engine.process_move(
            '''{"nodes" : [{
                  "id" : 1,
                  "owner" : "player_1",
                  "points" : "100",
                  "connections" : [2],
                  "starting_node" : "player_1"
                }, {
                  "id" : 2,
                  "owner" : "player_2",
                  "points" : "100",
                  "connections" : [1],
                  "starting_node" : "player_2"
                }]}''',
            '''{"moves" : [{
                  "node_to" : 3,
                  "owner" : "player_1",
                  "points" : "100"
                }]}'''
        )

        self.assertDictEqual(board, {u'nodes': [
            {
                u'id': 1,
                u'owner': u'player_1',
                u'points': 100,
                u'connections': [2],
                u'starting_node': u'player_1'
            },
            {
                u'starting_node': u'player_2',
                u'connections': [1],
                u'points': 100,
                u'owner': u'player_2',
                u'id': 2
            }
        ]})

        self.assertDictEqual(moves_processed, {u'moves': []})

        self.assertDictEqual(moves_with_errors, {u'moves': [
            {
                u'node_to': 3,
                u'owner': u'player_1',
                u'points': 100
            }
        ]})


    def test_check_one_invalid_move_missing_node_to(self):
        """
        """
        board, moves_processed, moves_with_errors = engine.process_move(
            '''{"nodes" : [{
                  "id" : 1,
                  "owner" : "player_1",
                  "points" : "100",
                  "connections" : [2],
                  "starting_node" : "player_1"
                }, {
                  "id" : 2,
                  "owner" : "player_2",
                  "points" : "100",
                  "connections" : [1],
                  "starting_node" : "player_2"
                }]}''',
            '''{"moves" : [{
                  "node_from" : 1,
                  "owner" : "player_1",
                  "points" : "100"
                }]}'''
        )

        self.assertDictEqual(board, {u'nodes': [
            {
                u'id': 1,
                u'owner': u'player_1',
                u'points': 100,
                u'connections': [2],
                u'starting_node': u'player_1'
            },
            {
                u'starting_node': u'player_2',
                u'connections': [1],
                u'points': 100,
                u'owner': u'player_2',
                u'id': 2
            }
        ]})

        self.assertDictEqual(moves_processed, {u'moves': []})

        self.assertDictEqual(moves_with_errors, {u'moves': [
            {
                u'node_from': 1,
                u'owner': u'player_1',
                u'points': 100
            }
        ]})

    def test_check_one_invalid_move_target(self):
        """
        """
        board, moves_processed, moves_with_errors = engine.process_move(
            '''{"nodes" : [{
                  "id" : 1,
                  "owner" : "player_1",
                  "points" : "100",
                  "connections" : [2],
                  "starting_node" : "player_1"
                }, {
                  "id" : 2,
                  "owner" : "player_2",
                  "points" : "100",
                  "connections" : [1],
                  "starting_node" : "player_2"
                }]}''',
            '''{"moves" : [{
                  "node_from" : 1,
                  "node_to" : 3,
                  "owner" : "player_1",
                  "points" : "100"
                }]}'''
        )

        self.assertDictEqual(board, {u'nodes': [
            {
                u'id': 1,
                u'owner': u'player_1',
                u'points': 100,
                u'connections': [2],
                u'starting_node': u'player_1'
            },
            {
                u'starting_node': u'player_2',
                u'connections': [1],
                u'points': 100,
                u'owner': u'player_2',
                u'id': 2
            }
        ]})

        self.assertDictEqual(moves_processed, {u'moves': []})

        self.assertDictEqual(moves_with_errors, {u'moves': [
            {
                u'node_from': 1,
                u'node_to': 3,
                u'owner': u'player_1',
                u'points': 100
            }
        ]})

    def test_check_one_invalid_move_points_overage(self):
        """
        """
        board, moves_processed, moves_with_errors = engine.process_move(
            '''{"nodes" : [{
                  "id" : 1,
                  "owner" : "player_1",
                  "points" : "100",
                  "connections" : [2],
                  "starting_node" : "player_1"
                }, {
                  "id" : 2,
                  "owner" : "player_2",
                  "points" : "100",
                  "connections" : [1],
                  "starting_node" : "player_2"
                }]}''',
            '''{"moves" : [{
                  "node_from" : 1,
                  "node_to" : 2,
                  "owner" : "player_1",
                  "points" : "101"
                }]}'''
        )

        self.assertDictEqual(board, {u'nodes': [
            {
                u'id': 1,
                u'owner': u'player_1',
                u'points': 100,
                u'connections': [2],
                u'starting_node': u'player_1'
            },
            {
                u'starting_node': u'player_2',
                u'connections': [1],
                u'points': 100,
                u'owner': u'player_2',
                u'id': 2
            }
        ]})

        self.assertDictEqual(moves_processed, {u'moves': []})

        self.assertDictEqual(moves_with_errors, {u'moves': [
            {
                u'node_from': 1,
                u'node_to': 2,
                u'owner': u'player_1',
                u'points': 101
            }
        ]})

    def test_check_one_invalid_move_invalid_owner(self):
        """
        """
        board, moves_processed, moves_with_errors = engine.process_move(
            '''{"nodes" : [{
                  "id" : 1,
                  "owner" : "player_1",
                  "points" : "100",
                  "connections" : [2],
                  "starting_node" : "player_1"
                }, {
                  "id" : 2,
                  "owner" : "player_2",
                  "points" : "100",
                  "connections" : [1],
                  "starting_node" : "player_2"
                }]}''',
            '''{"moves" : [{
                  "node_from" : 1,
                  "node_to" : 2,
                  "owner" : "player_3",
                  "points" : "100"
                }]}'''
        )

        self.assertDictEqual(board, {u'nodes': [
            {
                u'id': 1,
                u'owner': u'player_1',
                u'points': 100,
                u'connections': [2],
                u'starting_node': u'player_1'
            },
            {
                u'starting_node': u'player_2',
                u'connections': [1],
                u'points': 100,
                u'owner': u'player_2',
                u'id': 2
            }
        ]})

        self.assertDictEqual(moves_processed, {u'moves': []})

        self.assertDictEqual(moves_with_errors, {u'moves': [
            {
                u'node_from': 1,
                u'node_to': 2,
                u'owner': u'player_3',
                u'points': 100
            }
        ]})

    def test_check_one_valid_move(self):
        """
        """
        board, moves_processed, moves_with_errors = engine.process_move(
            '''
            {
                "nodes" : [
                    {
                        "id"            : 1,
                        "owner"         : "player_1",
                        "points"        : "100",
                        "connections"   : [ 2 ],
                        "starting_node" : "player_1"
                    },
                    {
                        "id"            : 2,
                        "owner"         : "player_2",
                        "points"        : "100",
                        "connections"   : [ 1 ],
                        "starting_node" : "player_2"
                    }
                ]
            }
            ''',
            '''
            {
                "moves" : [
                    {
                        "node_from" : 1,
                        "node_to" : 2,
                        "owner" : "player_1",
                        "points" : "10"
                    }
                ]
            }'''
        )

        self.assertDictEqual(board, {u'nodes': [
            {
                u'id': 1,
                u'owner': u'player_1',
                u'points': 90,
                u'connections': [2],
                u'starting_node': u'player_1'
            },
            {
                u'id': 2,
                u'owner': u'player_2',
                u'points': 90,
                u'connections': [1],
                u'starting_node': u'player_2'
            }
        ]})

        self.assertDictEqual(moves_processed, {u'moves': [
            {
                u'node_from': 1,
                u'node_to': 2,
                u'owner': u'player_1',
                u'points': 10
            }
        ]})

        self.assertDictEqual(moves_with_errors, {'moves': []})

    def test_check_one_valid_move_two_owners_same_target(self):
        """
        """
        board, moves_processed, moves_with_errors = engine.process_move(
            '''
            {
                "nodes" : [
                    {
                        "id"            : 1,
                        "owner"         : "player_1",
                        "points"        : "100",
                        "connections"   : [ 2, 3 ],
                        "starting_node" : "player_1"
                    },
                    {
                        "id"            : 2,
                        "owner"         : "player_2",
                        "points"        : "100",
                        "connections"   : [ 1, 3 ],
                        "starting_node" : "player_2"
                    },
                    {
                        "id"            : 3,
                        "owner"         : "player_3",
                        "points"        : "100",
                        "connections"   : [ 1, 2 ],
                        "starting_node" : "player_3"
                    }
                ]
            }
            ''',
            '''
            {
                "moves" : [
                    {
                        "node_from" : 1,
                        "node_to" : 2,
                        "owner" : "player_1",
                        "points" : "100"
                    },
                    {
                        "node_from" : 3,
                        "node_to" : 2,
                        "owner" : "player_3",
                        "points" : "100"
                    }
                ]
            }'''
        )

        self.assertDictEqual(board, {u'nodes': [
            {
                u'id': 1,
                u'owner': u'player_1',
                u'points': 0,
                u'connections': [2, 3],
                u'starting_node': u'player_1'
            },
            {
                u'id': 2,
                u'owner': u'player_2',
                u'points': 0,
                u'connections': [1, 3],
                u'starting_node': u'player_2'
            },
            {
                u'id': 3,
                u'owner': u'player_3',
                u'points': 0,
                u'connections': [1, 2],
                u'starting_node': u'player_3'
            }
        ]})

        self.assertDictEqual(moves_processed, {u'moves': [
            {
                u'node_from': 1,
                u'node_to': 2,
                u'owner': u'player_1',
                u'points': 100
            },
            {
                u'node_from': 3,
                u'node_to': 2,
                u'owner': u'player_3',
                u'points': 100
            }
        ]})

        self.assertDictEqual(moves_with_errors, {'moves': []})
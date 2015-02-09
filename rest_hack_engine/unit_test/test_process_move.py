import unittest
import process_move as engine

class TestRestHackEngine(unittest.TestCase):

    def setUp(self):
        """
        """

    def tearDown(self):
        """
        """

    def test_check_true(self):
        """
        """
        engine.process_move('{"nodes" : []}', '{"moves" : []}')
        self.assertTrue(True)
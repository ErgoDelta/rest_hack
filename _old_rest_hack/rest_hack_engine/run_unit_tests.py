#
# SYMANTEC: Copyright (c) 2015 Symantec Corporation. All rights reserved.
#
# THIS SOFTWARE CONTAINS CONFIDENTIAL INFORMATION AND TRADE SECRETS OF SYMANTEC
# CORPORATION.  USE, DISCLOSURE OR REPRODUCTION IS PROHIBITED WITHOUT THE PRIOR
# EXPRESS WRITTEN PERMISSION OF SYMANTEC CORPORATION.
#
# The Licensed Software and Documentation are deemed to be commercial computer
# software as defined in FAR 12.212 and subject to restricted rights as defined
# in FAR Section 52.227-19 "Commercial Computer Software - Restricted Rights"
# and DFARS 227.7202, Rights in "Commercial Computer Software or Commercial
# Computer Software Documentation," as applicable, and any successor regulations,
# whether delivered by Symantec as on premises or hosted services.  Any use,
# modification, reproduction release, performance, display or disclosure of
# the Licensed Software and Documentation by the U.S. Government shall be
# solely in accordance with the terms of this Agreement.
#

"""The main unit testing script for rest_hack_engine"""
import os
import unittest
import coverage
import argparse
import fileinput
import string
import shutil

import sys
# Suppress the .pyc files
sys.dont_write_bytecode = True

TEST_ARGS = {}


def unit_test_main():
    '''
    Main unit test function
    '''

    # Create and start code coverage
    if not TEST_ARGS['nocov']:
        cov = coverage.coverage(omit=['unit_test/*', '*__init__*',  "run_unit_tests.*"], branch=True)
        cov.start()

    # Load all modules in lib (This is ./lib in the controller directory)
    # This causes code coverage to report on modules that do not have unit tests

    # Use unit test to discover all module unit tests in ./unit_test
    test_suite = unittest.defaultTestLoader.discover('unit_test', 'test_*.py')

    # Run the test suite
    unittest.TextTestRunner(verbosity=TEST_ARGS['verbose']).run(test_suite)

    # Stop code coverage and save report
    if not TEST_ARGS['nocov']:
        cov.stop()
        if TEST_ARGS['xml']:
            if not os.path.exists(os.path.join('.', 'results/covxml')):
                os.makedirs(os.path.join('.', 'results/covxml'))
            cov.xml_report(outfile='./results/covxml/coverage.xml')
            for line in fileinput.input('./results/covxml/coverage.xml', inplace=True):
                if '<package ' in line:
                    print string.replace(line, 'name=""', 'name="controller"').rstrip()
                else:
                    print line.rstrip()
        if TEST_ARGS['html']:
            if os.path.exists(os.path.join('.', 'results/covhtml')):
                shutil.rmtree(os.path.join('.', 'results/covhtml'))
            cov.html_report(directory=os.path.join('.', 'results/covhtml'))

        # Output the results to the command line.
        cov.report()

if __name__ == '__main__':
    PARSER = argparse.ArgumentParser(description='Run controller test suites with code coverage reporting')
    PARSER.add_argument('-v', '--verbose', action='store_true',
                        help='Run with verbose unit testing output')
    PARSER.add_argument('--xml', action='store_true',
                        help='Run with xml coverage report output')
    PARSER.add_argument('--html', action='store_true',
                        help='Run with html output')
    PARSER.add_argument('--nocov', action='store_true',
                        help='Do not collect code coverage. Code coverage collection can interfere with debugging.')

    ARGS = PARSER.parse_args()
    D_ARGS = vars(ARGS)

    if D_ARGS['verbose']:
        TEST_ARGS['verbose'] = 2
    else:
        TEST_ARGS['verbose'] = 1

    if D_ARGS['xml']:
        TEST_ARGS['xml'] = True
    else:
        TEST_ARGS['xml'] = False

    if D_ARGS['html']:
        TEST_ARGS['html'] = True
    else:
        TEST_ARGS['html'] = False

    if D_ARGS['nocov']:
        TEST_ARGS['nocov'] = True
    else:
        TEST_ARGS['nocov'] = False

    unit_test_main()

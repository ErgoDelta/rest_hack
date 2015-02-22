#https://github.com/jbalogh/zamboni/blob/master/log_settings.py
"""An extended version of the log_settings module from zamboni:
"""
from __future__ import absolute_import

from tornado.log import LogFormatter as TornadoLogFormatter
import logging, logging.handlers
import os.path
import types

from logconfig import dictconfig

# Pulled from commonware.log we don't have to import that, which drags with
# it Django dependencies.
class RemoteAddressFormatter(logging.Formatter):
    """Formatter that makes sure REMOTE_ADDR is available."""

    def format(self, record):
        if ('%(REMOTE_ADDR)' in self._fmt
                and 'REMOTE_ADDR' not in record.__dict__):
            record.__dict__['REMOTE_ADDR'] = None
        return logging.Formatter.format(self, record)

class UTF8SafeFormatter(RemoteAddressFormatter):
    def __init__(self, fmt=None, datefmt=None, encoding='utf-8'):
        logging.Formatter.__init__(self, fmt, datefmt)
        self.encoding = encoding

    def formatException(self, e):
        r = logging.Formatter.formatException(self, e)
        if type(r) in [types.StringType]:
            r = r.decode(self.encoding, 'replace') # Convert to unicode
        return r

    def format(self, record):
        t = RemoteAddressFormatter.format(self, record)
        if type(t) in [types.UnicodeType]:
            t = t.encode(self.encoding, 'replace')
        return t

class NullHandler(logging.Handler):
    def emit(self, record):
        pass

def initialize_logging(syslog_tag,
                       syslog_facility,
                       loggers,
                       log_level=logging.INFO,
                       use_log=False):

    log_filename = './rest_hack.log'

    base_fmt = ('%(name)s:%(levelname)s %(message)s:%(pathname)s:%(lineno)s')

    cfg = {
        'version': 1,
        'filters': {},
        'formatters': {
            'debug': {
                '()': UTF8SafeFormatter,
                'datefmt': '%H:%M:%s',
                'format': '%(asctime)s ' + base_fmt,
            },
            'prod': {
                '()': UTF8SafeFormatter,
                'datefmt': '%H:%M:%s',
                'format': '%s: [%%(REMOTE_ADDR)s] %s' % (syslog_tag, base_fmt),
            },
            'tornado': {
                '()': TornadoLogFormatter,
                'color': True
            },
        },
        'handlers': {
            'console': {
                '()': logging.StreamHandler,
                'formatter': 'tornado'
            },
            'null': {
                '()': NullHandler,
            },
            'log': {
                '()': logging.FileHandler,
                'filename': log_filename,
                'formatter': 'prod',
            },
        },
        'loggers': {
        }
    }

    for key, value in loggers.items():
        cfg[key].update(value)

    # Set the level and handlers for all loggers.
    for logger in cfg['loggers'].values():
        if 'handlers' not in logger:
            logger['handlers'] = ['log' if use_log else 'console']
        if 'level' not in logger:
            logger['level'] = log_level
        if 'propagate' not in logger:
            logger['propagate'] = False

    dictconfig.dictConfig(cfg)

from handlers.base import BaseHandler

import logging
logger = logging.getLogger('boilerplate.' + __name__)


class FooHandler(BaseHandler):
    def get(self):
        msg = "Found base route!!!"
        logger.info(msg)
        self.render("base.html")

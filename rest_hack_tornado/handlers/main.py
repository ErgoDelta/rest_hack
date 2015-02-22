import json
import tornado.web

import logging
logger = logging.getLogger('boilerplate.' + __name__)


class MainHandler(BaseHandler):
    def get(self):
        if not self.current_user:
            self.redirect("/login")
            return
        name = tornado.escape.xhtml_escape(self.current_user)
        self.write("Hello, " + name)

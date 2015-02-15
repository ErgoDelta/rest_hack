#
import logging
from werkzeug.datastructures import CallbackDict
from flask.sessions import SessionInterface, SessionMixin
from itsdangerous import URLSafeTimedSerializer, BadSignature

#
class ItsDangerousSession(CallbackDict, SessionMixin):
    logger = logging.getLogger(__name__)

    def __init__(self, initial=None):
        self.logger.debug('Starting a ItsDangerousSession')
        def on_update(self):
            logger.debug('ItsdangerousSession was updated')
            self.modified = True
        CallbackDict.__init__(self, initial, on_update)
        self.modified = False


class ItsDangerousSessionInterface(SessionInterface):
    logger = logging.getLogger(__name__)

    salt = 'cookie-session'
    session_class = ItsDangerousSession

    def get_serializer(self, app):
        self.logger.debug('Getting serializer')
        if not app.secret_key:
            return None
        return URLSafeTimedSerializer(app.secret_key,
                                      salt=self.salt)

    def open_session(self, app, request):
        self.logger.debug('Opening session')
        s = self.get_serializer(app)
        if s is None:
            return None
        val = request.cookies.get(app.session_cookie_name)
        if not val:
            return self.session_class()
        max_age = app.permanent_session_lifetime.total_seconds()
        try:
            data = s.loads(val, max_age=max_age)
            return self.session_class(data)
        except BadSignature:
            return self.session_class()

    def save_session(self, app, session, response):
        self.logger.debug('Saving session')
        domain = self.get_cookie_domain(app)
        if not session:
            if session.modified:
                response.delete_cookie(app.session_cookie_name,
                                   domain=domain)
            return
        expires = self.get_expiration_time(app, session)
        val = self.get_serializer(app).dumps(dict(session))
        response.set_cookie(app.session_cookie_name, val,
                            expires=expires, httponly=True,
                            domain=domain)

#
import logging
import hashlib
from flask import Flask, current_app

#
app = Flask(__name__)
logger = logging.getLogger(__name__)

#
def hash_pass(password):
    with app.app_context():
        if password and app.secret_key:
            salted_password = password + app.secret_key
            m = hashlib.md5()
            m.update(salted_password)
            return m.digest()
        return None

#
import logging
from flask import Flask, current_app
from flask_login import UserMixin
from itsdangerous import URLSafeTimedSerializer
#
from util.pass_hash import hash_pass

#
app = Flask(__name__)
logger = logging.getLogger(__name__)

#Create a quick list of users (username, password).  The password is stored
#as a md5 hash that has also been salted.  You should never store the users
#password and only store the password after it has been hashed.
USERS = (
        ("user1", hash_pass("pass1")),
        ("user2", hash_pass("pass2"))
        )

#Login_serializer used to encryt and decrypt the cookie token for the remember
#me option of flask-login
with app.app_context():
    login_serializer = URLSafeTimedSerializer(app.secret_key)

class User(UserMixin):
    """
    User Class for flask-Login
    """
    def __init__(self, userid, password):
        self.id = userid
        self.password = password

    def get_auth_token(self):
        """
        Encode a secure token for cookie
        """
        data = [str(self.id), self.password]
        return login_serializer.dumps(data)

    @staticmethod
    def get(userid):
        """
        Static method to search the database and see if userid exists.  If it
        does exist then return a User Object.  If not then return None as
        required by Flask-Login.
        """
        #For this example the USERS database is a list consisting of
        #(user,hased_password) of users.
        for user in USERS:
            if user[0] == userid:
                return User(user[0], user[1])
        return None

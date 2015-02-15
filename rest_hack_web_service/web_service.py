#!flask/bin/python
#
import argparse
import logging
from datetime import timedelta
from flask import Flask
#from flask.ext.pymongo import PyMongo
#
from http_handlers.error_handler import error_handler
#
from controllers.index import index
from controllers.user import user
#
#from util.sessions import ItsDangerousSession, ItsDangerousSessionInterface

#
args = None

#
app = Flask(__name__)
#mongo = PyMongo(app)

#
app.register_blueprint(error_handler)
app.register_blueprint(index)
app.register_blueprint(user)
#
#app.session_interface = ItsDangerousSessionInterface()

#login_manager = LoginManager()
#login_manager.init_app(app)

@index.route('/')
def index_page():
    user_id = (current_user.get_id() or "No User Logged In")
    return jsonify({'user_id': user_id})

# main to run program
if __name__ == '__main__':
    parser = argparse.ArgumentParser(description='This is a small Flask RESTfull web service for the rest_hack project.')
    parser.add_argument('--flask_host',
                        default='0.0.0.0',
                        help='The host IP of the server, but defualt this is set to "0.0.0.0" to make this instance public.')
    parser.add_argument('--flask_port',
                        default=8081,
                        help='The port that this flask instance should be stood up under, byt defualt it is set to "8081".')
    parser.add_argument('--flask_debug',
                        default=False,
                        help='Wether of not to turn on Flasks own internal debuging.')
    parser.add_argument('--log_name',
                        default='web_service.log',
                        help='Name of the log file to output all log infromation to.')
    parser.add_argument('--logging_level',
                        default=logging.DEBUG,
                        help='The level of loggin to capture to the given log file.')
    args = parser.parse_args()

    #Change the duration of how long the Remember Cookie is valid on the users
    #computer.  This can not really be trusted as a user can edit it.
    app.config["REMEMBER_COOKIE_DURATION"] = timedelta(days=14)

    logging.basicConfig(filename=args.log_name, level=args.logging_level)
    app.secret_key = 'MIIEoQIBAAKCAQBSwlp9gtvnOL0Pv4h3hAzFS5kOZgzAIHsrB67zx19Kail1rFBJqAm9WQyFJu6OZJeAonFXwPmjUC2+1OQlL3VnaDHmk6eo3z6wIMKUJMn1Gs7VnYSk08oPdJgxoPKyc4caGU3IJsInqvkCdkkY8crGlGxRkUUPpJtMjRqmyRNJdY/R0ijAjbYqXuoIhyUfQEDylCHrGyGivHqMLeDX96Y9R76L5GZ6Cc8RFOyWURLGyWwpRhli5tzHUkZvKuoWsoBEp5yJaFrNsP8v6U6fZtj09yGl3GTxldbn10ex3kpYT2lailNxTIzS+s3pCrGwTq11cjPznbxDAj4LzAr2M7g9AgMBAAECggEAJLBg6Y+f6avvtRGWcFWsS7MuEYIQWQSNVhA78XsLtbPG2ZIgrea/GqBluon97EaQG8joMp0sjsg234kF865MgCFjLfN7upPt+KstV/vPRMgzteP1MBVsWNm5txbBQXeSB3H8V6VHIcrcRckmstZOrSaTtr2OPlcdOhy8GgC+eiwRAS20+xdCZQiyPVOPzxUz+clYi807hMtcntOo18+iUqoONOOQr9+sVcmdrxL2Re0aLK+06BwZW3kQtCxSToZ5xYmR9MU+nREUor8g1ZD7BswBTcDObYlUPcCRRQfboTH4E3yxeHDp0+UevFBJ7NVBEuFSle4BEXdqrXEqDqacwQKBgQCXtr/ptPBo5nfVhkkR/1n5RhaC1xOcoBhHqvq7T6fc48V9pJqTWDSLCFdAGUWLE0eWfq1DFTgq+opBD6zkk6iwQw7V/+dLDIrC15OOQUMck9w9jdJkBI8vIfUiXHWOqKyDgTYVaP1s+jY62YTPIKg/mfL5ELV5uqQ0DOw0bKbNyQKBgQCLpZOlVgyXb2cT5EuHNwvgc298PxCr65VmsBDxzEJf7hjhvAKHRdmZWUaInvHcLMosbFIWQbC5zeNH/gOTwIiRScQhiTYsBJxF3NbtUPWu8/pyhRpWg8COOolI7zCDEuOUDP21ReDS3Xt4qtZhv9rAMBkzhtxJF8YZCZuXT1aA1QKBgC3iYqafR7qiQYn9xCnCTgb7MKG/xLmxHcbHKm/uRnmjFb7LQsjfe3y1OBY1zneijBszEtngGA2/moLU59h15CXzEhCdBeoH0AvfwUvb5x1Ehu7C7ue7DUcXARm0VwWfdBWNxbqx9zu3bSFdWjJ+0QPzXq3/ZLN9RF5Nrj6owcYpAoGAc8ROBBCz2461Cx6FJpERvX60+3FkpsFkV366bVmB5PkDk49DIVVcsO37tSLfKkHPUMhzvJO0qDPwqSwBVymTja5zc8HGMPOZgak0XARzyBfmla2WSgZrSP1p9hakRuUP2Rpz8ST+3pBR3ZTyqJJeDFlTaw3v7IBS70qxu9w/wAkCgYA5duET8sUWUbFD0tFgcPuiS3QaCIfOcRA8Ts3M3sEntgdBdpuScEBGmhASkpmr6uhlkoQN9LJjawvqrMH8whwdt2A+xLiAL0hgkosCAmfffScL7U94vJNiM55edi8v8LgEjO+ojuCzU9dIimeuLciWK5pKBl0xWtdE2h2njv2xSg'
    app.run(debug=args.flask_debug ,host=args.flask_host, port=args.flask_port)

#
import logging
from flask import Blueprint, jsonify, make_response

#
logger = logging.getLogger(__name__)
error_handler = Blueprint('error_handler', __name__)

#
@error_handler.errorhandler(401)
def not_found(error):
    logger.error('401 : Unauthorized')
    return make_response(jsonify({'error': 'Unauthorized'}), 401)

@error_handler.errorhandler(403)
def not_found(error):
    logger.error('403 : Forbidden')
    return make_response(jsonify({'error': 'Forbidden'}), 403)

@error_handler.errorhandler(404)
def not_found(error):
    logger.error('404 : Not Found')
    return make_response(jsonify({'error': 'Not found'}), 404)

@error_handler.errorhandler(405)
def not_found(error):
    logger.error('405 : Method Not Allowed')
    return make_response(jsonify({'error': 'Method Not Allowed'}), 405)

@error_handler.errorhandler(406)
def not_found(error):
    logger.error('406 : Not Acceptable')
    return make_response(jsonify({'error': 'Not Acceptable'}), 406)

@error_handler.errorhandler(408)
def not_found(error):
    logger.error('408 : Request Timeout')
    return make_response(jsonify({'error': 'Request Timeout'}), 408)

@error_handler.errorhandler(429)
def not_found(error):
    logger.error('429 : Too Many Requests')
    return make_response(jsonify({'error': 'Too Many Requests'}), 429)

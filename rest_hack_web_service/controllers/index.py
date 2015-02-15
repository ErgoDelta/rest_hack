#
import logging
from flask import Blueprint, jsonify

#
logger = logging.getLogger(__name__)
index = Blueprint('index', __name__)

#
@index.route('/index', methods=['GET'])
def get_index():
    logger.debug('/index')
    return jsonify({'message': 'Hello World'})

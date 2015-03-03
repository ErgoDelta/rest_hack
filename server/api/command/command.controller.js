'use strict';

var _ = require('lodash');
var Game = require('./command.model');

exports.create = function(req, res) {
  // Get the current game state
};

function handleError(res, err) {
  return res.send(500, err);
}

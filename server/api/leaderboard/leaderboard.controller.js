
'use strict';

var _ = require('lodash');
var Leaderboard = require('./leaderboard.model');

// Get list of games
exports.index = function(req, res) {
  Leaderboard.find(function (err, games) {
  if(err) { return handleError(res, err); }
  return res.json(200, games);
  });
};

function handleError(res, err) {
  return res.send(500, err);
}

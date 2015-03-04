'use strict';

var mongoose = require('mongoose'),
Schema = mongoose.Schema;

var LeaderboardSchema = new Schema({
  teamName: String,
  gamesPlayed: Number,
  gamesWon: Number
});

module.exports = mongoose.model('Leaderboard', LeaderboardSchema);

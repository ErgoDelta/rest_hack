/**
 * Populate DB with sample data on server start
 * to disable, edit config/environment/index.js, and set `seedDB: false`
 */

'use strict';

var Game = require('../api/game/game.model');
var User = require('../api/user/user.model');

Game.find({}).remove(function() {
  Game.create({
    name : 'First Blood',
    info : 'This is the first game you will face!'
  }, {
    name : 'Mad Max',
    info : '...'
  }, {
    name : 'Mad Max 2',
    info : '...'
  }, {
    name : 'Mad Max Beyond Thunderdome',
    info : '...'
  }, {
    name : 'Mad Max: Fury Road',
    info : 'What awaits behind door number two?'
  });
});

User.find({}).remove(function() {
  User.create({
    provider: 'local',
    name: 'Test User',
    email: 'test@test.com',
    password: 'test'
  }, {
    provider: 'local',
    role: 'admin',
    name: 'Admin',
    email: 'admin@admin.com',
    password: 'admin'
  }, function() {
      console.log('finished populating users');
    }
  );
});

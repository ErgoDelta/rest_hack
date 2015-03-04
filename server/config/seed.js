/**
 * Populate DB with sample data on server start
 * to disable, edit config/environment/index.js, and set `seedDB: false`
 */

'use strict';

var Game = require('../api/game/game.model');
var User = require('../api/user/user.model');
var Leaderboard = require('../api/leaderboard/leaderboard.model');

Game.find({}).remove(function() {
  Game.create({
    name : 'First Blood',
    info : 'This is the first game you will face!',
    world : {
      "nodes" : [
        {
          "id" : 0,
          "owner" : "player_1",
          "points" : "100",
          "connections" : [
          1,
          3,
          4
          ],
        },
        {
          "id" : 1,
          "owner" : "player_1",
          "points" : "100",
          "connections" : [
          5,
          2,
          3,
          0
          ],
          "starting_node" : "player_1"
        },
        {
          "id" : 2,
          "owner" : "player_2",
          "points" : "100",
          "connections" : [
          1,
          5
          ],
          "starting_node" : "player_2"
        },
        {
          "id" : 3,
          "owner" : "player_3",
          "points" : "100",
          "connections" : [
          7,
          1,
          0
          ],
          "starting_node" : "player_3"
        },
        {
          "id" : 4,
          "owner" : "player_4",
          "points" : "100",
          "connections" : [
          6,
          8,
          0
          ],
          "starting_node" : "player_4"
        },
        {
          "id" : 5,
          "owner" : "none",
          "points" : "0",
          "connections" : [
          1,
          6,
          7,
          2
          ]
        },
        {
          "id" : 6,
          "owner" : "none",
          "points" : "0",
          "connections" : [
          4,
          5,
          7
          ]
        },
        {
          "id" : 7,
          "owner" : "none",
          "points" : "0",
          "connections" : [
          3,
          6,
          8
          ]
        },
        {
          "id" : 8,
          "owner" : "none",
          "points" : "0",
          "connections" : [
          4,
          6,
          7
          ]
        }
      ]
    }
  }, {
    name : 'Mad Max',
    info : '...',
    world : {}
  }, {
    name : 'Mad Max 2',
    info : '...',
    world : {}
  }, {
    name : 'Mad Max Beyond Thunderdome',
    info : '...',
    world : {}
  }, {
    name : 'Mad Max: Fury Road',
    info : '...',
    world : {}
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

Leaderboard.find({}).remove(function() {
  Leaderboard.create({
    teamName: 'James',
    gamesPlayed: 40,
    gamesWon: 20
  }, {
    teamName: 'Brad',
    gamesPlayed: 67,
    gamesWon: 34
  },{
    teamName: 'Payden',
    gamesPlayed: 46,
    gamesWon: 11
  }, {
    teamName: 'Anthony',
    gamesPlayed: 39,
    gamesWon: 29
  }, {
    teamName: 'Kirk',
    gamesPlayed: 56,
    gamesWon: 12
  }, {
    teamName: 'Chris',
    gamesPlayed: 76,
    gamesWon: 19
  })
});

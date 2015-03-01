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
    info : 'This is the first game you will face!',
    world : {
      "nodes" : [
        {
          "id" : 1,
          "owner" : "player_1",
          "points" : "100",
          "connections" : [
          5
          ],
          "starting_node" : "player_1"
        },
        {
          "id" : 2,
          "owner" : "player_2",
          "points" : "100",
          "connections" : [
          6
          ],
          "starting_node" : "player_2"
        },
        {
          "id" : 3,
          "owner" : "player_3",
          "points" : "100",
          "connections" : [
          7
          ],
          "starting_node" : "player_3"
        },
        {
          "id" : 4,
          "owner" : "player_4",
          "points" : "100",
          "connections" : [
          8
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
          7
          ]
        },
        {
          "id" : 6,
          "owner" : "none",
          "points" : "0",
          "connections" : [
          2,
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

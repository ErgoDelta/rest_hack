'use strict';

var express = require('express');
var controller = require('./game.controller');
var config = require('../../config/environment');
var auth = require('../../auth/auth.service');
//var game_engine = require('./../../components/game_engine');

var router = express.Router();

//
router.post('/', auth.hasRole('admin'), controller.create);
router.delete('/:id', auth.hasRole('admin'), controller.destroy);

//
router.get('/', auth.isAuthenticated(), controller.index);
router.get('/:id', auth.isAuthenticated(), controller.show);
router.put('/:id', auth.isAuthenticated(), controller.update);


module.exports = router;

'use strict';

angular
  .module('restHackApp')
  .config(function ($stateProvider) {
    $stateProvider
      .state('gameCreate', {
        url: '/game/create',
        templateUrl: 'app/game/create.html',
        controller: 'CreateGameCtrl'
      })
      .state('gameDelete', {
        url: '/game/:id/delete',
        templateUrl: 'app/game/delete.html',
        controller: 'DeleteGameCtrl',
        authenticate: true
      })
      .state('gameView', {
        url: '/game/:id/view',
        templateUrl: 'app/game/view.html',
        controller: 'ViewGameCtrl'
      })
      .state('gameMove', {
        url: '/game/:id/move',
        templateUrl: 'app/game/move.html',
        controller: 'MoveGameCtrl'
      });
  });

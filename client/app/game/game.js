'use strict';

angular.module('restHackApp')
.config(function ($stateProvider) {
  $stateProvider
  .state('game.create', {
    url: '/game/create',
    templateUrl: 'app/game/create/create.html',
    controller: 'CreateGameCtrl'
  })
  .state('game.delete', {
    url: '/game/:id/delete',
    templateUrl: 'app/game/delete/delete.html',
    controller: 'DeleteGameCtrl',
    authenticate: true
  })
  .state('game.view', {
    url: '/game/:id/view',
    templateUrl: 'app/game/view/view.html',
    controller: 'ViewGameCtrl'
  })
  .state('game.move', {
    url: '/game/:id/move',
    templateUrl: 'app/game/move/move.html',
    controller: 'MoveGameCtrl'
  });
});

'use strict';

angular.module('restHackApp')
.controller('ViewGameCtrl', function ($scope, $stateParams, Auth) {

  $http.get('/api/games/' + $stateParams.id).success(function(game) {
    $scope.game = game;
    //socket.syncUpdates('game', $scope.game);
  });
});

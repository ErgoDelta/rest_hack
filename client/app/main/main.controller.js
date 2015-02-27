'use strict';

angular.module('restHackApp')
  .controller('MainCtrl', function ($scope, $http, $state, socket, Auth) {
    $scope.games = [];
    $scope.isAdmin = Auth.isAdmin;

    $http.get('/api/games').success(function(games) {
      $scope.games = games;
      socket.syncUpdates('game', $scope.games);
    });

    $scope.viewGame = function(gameId) {
      $state.go('game.view', {id:gameId})
    };

    $scope.addGame = function() {
      if($scope.newGame === '') {
        return;
      }
      $http.post('/api/games', { name: $scope.newGame });
      $scope.newGame = '';
    };

    $scope.deleteGame = function(game) {
      $http.delete('/api/games/' + game._id);
    };

    $scope.$on('$destroy', function () {
      socket.unsyncUpdates('game');
    });
  });

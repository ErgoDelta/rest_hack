'use strict';

angular.module('restHackApp')
.controller('CreateGameCtrl', function ($scope, Auth) {

  $scope.createGame = function(form) {
    $scope.submitted = true;

    if(form.$valid) {
      //
    }
  };

});

'use strict';

angular.module('restHackApp')
.controller('DeleteGameCtrl', function ($scope, User, Auth) {
  $scope.errors = {};

  $scope.deleteGame = function(form) {
    $scope.submitted = true;
    if(form.$valid) {
      //
    }
  };
});

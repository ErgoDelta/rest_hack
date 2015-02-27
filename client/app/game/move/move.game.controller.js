'use strict';

angular.module('restHackApp')
.controller('MoveGameCtrl', function ($scope, Auth) {
  $scope.errors = {};

  $scope.moveGame = function(form) {
    $scope.submitted = true;
    if(form.$valid) {
      //
    }
  };
});

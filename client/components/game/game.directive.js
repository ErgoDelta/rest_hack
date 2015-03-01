'use strict';

/**
* Used to draw and render the game as well as take in games inputs in the view.
*/
angular
  .module('restHackApp')
  .directive("game",
    function() {
      return {
        restrict: "EA",
        scope: { game: '=' },
        link: function(scope, element, attr) {
          var canvas = document.getElementById('game_canvas'),
              context = canvas.getContext('2d');

          function renderNode(node) {
            console.log(node);
            var centerX = 0,
                centerY = 0,
                radius = 20;

            context.beginPath();
            context.fillStyle = "black";
            //context.arc(0 + (node.id * 2), 0 + (node.id * 2), radius, 0, Math.PI*2, true);
            context.rect(0 + (node.id * 2), 0 + (node.id * 2), 5, 5);
            //context.lineWidth = 5;
            //context.strokeStyle = '#003300';
            context.closePath();
            context.fill();

            context.beginPath();
            context.fillStyle = "red";
            //context.arc(5 + (node.id), 5 + (node.id), radius * 2, 0, Math.PI*2, true);
            context.rect(5 + (node.id * 2), 5 + (node.id * 2), 10, 10);
            context.lineWidth = 4;
            //context.strokeStyle = '#003300';
            //context.closePath();
            context.fill();
            console.log('render');
          }

          scope.$watch("game", function(newValue, oldValue) {
            if(scope.game) {
              console.log(scope.game);
              console.log(scope.game.world);
              console.log(scope.game.world.nodes);
              for (var node in scope.game.world.nodes) {
                renderNode(node);
              }
            }
            console.log(element);
            console.log(context);
          }, true);
      }
    }
  });

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

          function renderNode(nodes) {
            var maxPerRow = Math.sqrt(nodes.length);
            var startingX = (canvas.width / maxPerRow) / 2;
            var startingY = (canvas.height / maxPerRow) / 2;

            for (var y = 0; y < maxPerRow; y++) {
              for (var x = 0; x < maxPerRow; x++) {
                context.arc(startingX, startingY,20,0,2*Math.PI);
                context.stroke();
                startingX += (canvas.width / maxPerRow);
              }
            startingY += (canvas.height / maxPerRow);
            startingX = (canvas.width / maxPerRow) / 2;
            }
          }

          scope.$watch("game", function(newValue, oldValue) {
            if(scope.game) {
              renderNode(scope.game.world.nodes);
            }
          }, true);
      }
    }
  });

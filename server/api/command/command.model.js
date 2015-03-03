'use strict';

var mongoose = require('mongoose'),
Schema = mongoose.Schema;

var CommandSchema = new Schema({
  from_node_id: Number,
  to_node_id: Number,
  number_of_units: Number
});

module.exports = mongoose.model('Command', CommandSchema);

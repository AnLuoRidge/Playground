// 1st Try
// Runtime: 112 ms, faster than 39.33% of JavaScript online submissions for Design Circular Deque.
// Memory Usage: 41.4 MB, less than 100.00% of JavaScript online submissions for Design Circular Deque.

/**
 * Initialize your data structure here. Set the size of the deque to be k.
 * @param {number} k
 */
var MyCircularDeque = function(k) {
  this.deque = [];
  this.length = k;
};

/**
 * Adds an item at the front of Deque. Return true if the operation is successful. 
 * @param {number} value
 * @return {boolean}
 */
MyCircularDeque.prototype.insertFront = function(value) {
  if (this.deque.length < this.length) {
    this.deque.splice(0, 0, value);
    return true;
  } else {
    return false; //'false, the queue is full';
  }
};

/**
 * Adds an item at the rear of Deque. Return true if the operation is successful. 
 * @param {number} value
 * @return {boolean}
 */
MyCircularDeque.prototype.insertLast = function(value) {
  if (this.deque.length < this.length) {
    this.deque.push(value);
    return true;
  } else {
    return false; //'false, the queue is full';
  }
};

/**
 * Deletes an item from the front of Deque. Return true if the operation is successful.
 * @return {boolean}
 */
MyCircularDeque.prototype.deleteFront = function() {
  if (this.deque.length > 0) {
    this.deque.splice(0, 1);
    return true;
  } else {
    return false;
  }
};

/**
 * Deletes an item from the rear of Deque. Return true if the operation is successful.
 * @return {boolean}
 */
MyCircularDeque.prototype.deleteLast = function() {
  if (this.deque.length > 0) {
    this.deque.pop();
    return true;
  } else {
    return false;
  }
};

/**
 * Get the front item from the deque.
 * @return {number}
 */
MyCircularDeque.prototype.getFront = function() {
  if (this.deque.length > 0) {
    return this.deque[0];
  } else {
    return -1;
  }
};

/**
 * Get the last item from the deque.
 * @return {number}
 */
MyCircularDeque.prototype.getRear = function() {
  if (this.deque.length > 0) {
    return this.deque[this.deque.length - 1];
  } else {
    return -1;
  }
};

/**
 * Checks whether the circular deque is empty or not.
 * @return {boolean}
 */
MyCircularDeque.prototype.isEmpty = function() {
  return this.deque.length <= 0;
};

/**
 * Checks whether the circular deque is full or not.
 * @return {boolean}
 */
MyCircularDeque.prototype.isFull = function() {
  return this.deque.length === this.length;
};

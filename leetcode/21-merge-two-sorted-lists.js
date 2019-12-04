/**
 * Definition for singly-linked list.
 * function ListNode(val) {
 *     this.val = val;
 *     this.next = null;
 * }
 */
/**
 * @param {ListNode} l1
 * @param {ListNode} l2
 * @return {ListNode}
 */
// constructor pattern

var mergeTwoLists = function(l1, l2) {
    let c1 = l1, c2 = l2, l3 = new ListNode(null);
    let c3 = l3;
    let k1 = 1, k2 = 1;
    //     while (c1.val && c2.val) { NULL cannot be checked in while loop
    while (k1 || k2) {
        if (c1.val <= c2.val) {
            c3.next = new ListNode(c1.val);
            c3 = c3.next;
            c1 = c1.next;
        } else {
            c3.next = new ListNode(c2.val);
            c3 = c3.next;
            c2 = c2.next;
        }
        if (!c1) {
            k1 = 0;
            // paste c2 to c3
            break;
        } 
        if (!c2) {
            k2 = 0;
            break;
        }
    }
    return l3.next;
};
// 2 > null TRUE
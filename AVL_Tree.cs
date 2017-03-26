﻿/**
 *  This implements the basic function of AVL Tree.
 *  @author Davin-Yu
 */

using System;

namespace AVL_Tree_script
{
    /* Node Structure */
    public class Node
    {
        private Node parent;
        private Node leftchild;
        private Node rightchild;
        private int value;
        private int height;

        public Node(int v, Node p) {
            this.parent = p;
            this.leftchild = null;
            this.rightchild = null;
            this.value = v;
            this.height = 1;
        }

        public void setParent(Node p) {
            this.parent = p;
        }

        public void setLeftChild(Node node) {
            this.leftchild = node;
        }

        public void setRightChild(Node node) {
            this.rightchild = node;
        }

        public void setValue(int v) {
            this.value = v;
        }

        public void setHeight(int b) {
            this.height = b;
        }

        public Node getParent() {
            return this.parent;
        }

        public Node getLeftChild() {
            return this.leftchild;
        }

        public Node getRightChild() {
            return this.rightchild;
        }

        public int getValue() {
            return this.value;
        }

        public int getHeight() {
            return this.height;
        }
    }

    /* AVL_Tree */
    public class AVL_Tree
    {
        public Node root;
        public AVL_Tree() {
            root = null;
        }

        public void printAll(Node x) {
            if (x != null) {
                System.Console.Write("Node:" + x.getValue() + " ");
                System.Console.Write("Height:" + x.getHeight() + " ");
                if (x.getParent() != null) {
                    System.Console.Write("Parent:" + x.getParent().getValue() + " ");
                }else {
                    System.Console.Write("!Root! ");
                }
            }
            if (x.getLeftChild() != null) {
                System.Console.Write("LeftChild:" + x.getLeftChild().getValue() + " ");
            }
            if (x.getRightChild() != null) {
                System.Console.Write("RightChild:" + x.getRightChild().getValue());
            }
            System.Console.WriteLine();
            if (x.getLeftChild() != null) {
                printAll(x.getLeftChild());
            }
            if (x.getRightChild() != null) {
                printAll(x.getRightChild());
            }
        }

        public int getLeftSonHeight(Node x) {
            if (x.getLeftChild() != null) {
                return x.getLeftChild().getHeight();
            }else {
                return 0;
            }
        }

        public int getRightSonHeight(Node x) {
            if (x.getRightChild() != null) {
                return x.getRightChild().getHeight();
            }
            else {
                return 0;
            }
        }

        public int sonMaxHeight(Node x) {
            int maxHeight = 0;
            if (x.getRightChild() != null) {
                maxHeight = x.getRightChild().getHeight();
            }
            if ((x.getLeftChild() != null) && (x.getLeftChild().getHeight()>maxHeight)) {
                maxHeight = x.getLeftChild().getHeight();
            }
            return maxHeight;
        }

        public void setNewHeight(Node tempRoot)
        {
            tempRoot.getLeftChild().setHeight(sonMaxHeight(tempRoot.getLeftChild()) + 1);
            tempRoot.getRightChild().setHeight(sonMaxHeight(tempRoot.getRightChild()) + 1);
            tempRoot.setHeight(sonMaxHeight(tempRoot));
        }

        public void RightRotate(Node _root, Node _povit) {
            Node p = _root.getParent();
            _root.setLeftChild(_povit.getRightChild());
            if (_povit.getRightChild() != null) {
                _povit.getRightChild().setParent(_root);
            }
            _povit.setRightChild(_root);
            _root.setParent(_povit);
            if ((p != null) && (p.getLeftChild() != null) && (p.getLeftChild().Equals(_root))) {
                p.setLeftChild(_povit);
            } else if (p != null) {
                p.setRightChild(_povit);
            }
            _povit.setParent(p);
        }

        public void LeftRotate(Node _povit, Node _root) {
            Node p = _root.getParent();
            _root.setRightChild(_povit.getLeftChild());
            if (_povit.getLeftChild() != null) {
                _povit.getLeftChild().setParent(_root);
            }
            _povit.setLeftChild(_root);
            _root.setParent(_povit);
            if ((p != null) && (p.getLeftChild() != null) && (p.getLeftChild().Equals(_root))) {
                p.setLeftChild(_povit);
            }else if (p != null) {
                p.setRightChild(_povit);
            }
            _povit.setParent(p);
        }

        public void HeightRenew(Node temp) {
            Node parent;
            while (temp.getParent() != null) {
                parent = temp.getParent();
                int leftHeight;
                int rightHeight;
                if (parent.getLeftChild() != null) {
                    leftHeight = parent.getLeftChild().getHeight();
                } else {
                    leftHeight = 0;
                }
                if (parent.getRightChild() != null) {
                    rightHeight = parent.getRightChild().getHeight();
                } else {
                    rightHeight = 0;
                }
                if (Math.Abs(rightHeight - leftHeight) >= 2) {
                    restructure(parent);
                } else {
                    if (rightHeight > leftHeight) {
                        parent.setHeight(rightHeight + 1);
                    } else {
                        parent.setHeight(leftHeight + 1);
                    }
                    temp = temp.getParent();
                }
            }
            root = temp;
        }

        public void restructure(Node ubNode) {
            int ubNoderighth, ubNodelefth;
            ubNoderighth = getRightSonHeight(ubNode);
            ubNodelefth = getLeftSonHeight(ubNode);
            /* Left Case */
            if (ubNoderighth < ubNodelefth) {
                Node ubLNode = ubNode.getLeftChild();
                int ubLNoderighth, ubLNodelefth;
                ubLNoderighth = getRightSonHeight(ubLNode);
                ubLNodelefth = getLeftSonHeight(ubLNode);
                /* Left-Left Case */
                if (ubLNoderighth < ubLNodelefth) {
                    RightRotate(ubNode, ubLNode);
                    Node tempRoot = ubLNode;
                    setNewHeight(tempRoot);
                }
                /* Left-Right Case*/
                else {
                    LeftRotate(ubLNode.getRightChild(), ubLNode);
                    ubLNode = ubNode.getLeftChild();
                    RightRotate(ubNode, ubLNode);
                    Node tempRoot = ubLNode;
                    setNewHeight(tempRoot);
                }
            }
            /* Right Case */
            else {
                Node ubRNode = ubNode.getRightChild();
                int ubRNoderighth, ubRNodelefth;
                ubRNoderighth = getRightSonHeight(ubRNode);
                ubRNodelefth = getLeftSonHeight(ubRNode);
                /* Right-Right Case */
                if (ubRNodelefth < ubRNoderighth) {
                    LeftRotate(ubRNode, ubNode);
                    Node tempRoot = ubRNode;
                    setNewHeight(tempRoot);
                }
                /* Right-Left Case */
                else {
                    RightRotate(ubRNode, ubRNode.getLeftChild());
                    ubRNode = ubNode.getRightChild();
                    LeftRotate(ubRNode, ubNode);
                    Node tempRoot = ubRNode;
                    setNewHeight(tempRoot);
                }
            }
            HeightRenew(ubNode);
        }

        public bool insert(int v) {
            if (root == null) {
                root = new Node(v, null);
            }else {
                Node temp = root;
                bool found = false;
                /* find the new node location */
                while (!found) {
                    if (v < temp.getValue()) {
                        if (temp.getLeftChild() != null) {
                            temp = temp.getLeftChild();
                        }else {
                            found = true;
                            temp.setLeftChild(new Node(v, temp));
                            if (temp.getRightChild() == null) {
                                temp.setHeight(2);
                            }
                        }
                    } else {
                        if (temp.getRightChild() != null) {
                            temp = temp.getRightChild();
                        }else {
                            found = true;
                            temp.setRightChild(new Node(v, temp));
                            if (temp.getLeftChild() == null) {
                                temp.setHeight(2);
                            }
                        }
                    }
                }
                HeightRenew(temp);
                return true;
            }
            return false;
        }

        public bool delete(int v) {

            return false;
        }
    }

    public class main {
        public static void Main(string[] args)
        {
            AVL_Tree avl = new AVL_Tree();
            while (true) {
                int newvalue = Convert.ToInt32(Console.ReadLine());
                avl.insert(newvalue);
                avl.printAll(avl.root);
            }
        }
    }
}

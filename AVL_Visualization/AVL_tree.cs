/**
 *  This program implements AVL Tree Data Structure.
 *  The AVL Tree supports search, insert and delete operation.
 *  <p>
 *  People who read this code should first be familiar with AVL_Tree and its multiplication.
 *  Such as search, insert, delete, right rotation, left rotation and restructure.
 *  Copyright 2017 Davin Yu
 *
 *  @author Davin-Yu
 */

using System;

namespace AVL_Tree_script
{
    /**
     *  This class implements the Node in a Tree structrue. Each node contains
     *  the information of its parent, leftchild, rightchild, with its own value
     *  and height. It also allow this information to be set or gotten.
     *  This class acts as support class for {@link AVL_Tree}
     */
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

    /**
     *  This class implements an AVL_Tree which allows searching, inserting, deleting.
     */
    public class AVL_Tree
    {
        public Node root;
        public AVL_Tree() {
            root = null;
        }

        /**
         *  Print all Nodes with their own information in the AVL_Tree.
         *  It recursively print all Nodes in the AVL_Tree in Depth First Search
         *  resluting order. It provides information like Node value, Node height,
         *  parent, children.
         *
         *  @param x  The Node is currently under printing.
         */
        public void printAll(Node x) {
            if (x == null) {
                return;
            }
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

        /**
         *  Deletion one Node to by replcing with another Node if exsits
         *  @param  temp  the Node which will be deleted
         *  @param  _to   the Node which will replace the deleted Node
         */
        public void deleteNodeTo(Node temp, Node _to) {
            //在这个过程中, temp是会被删除的点, 最后他会被设成Null！！！！
            Node tempf = temp.getParent();  //得到被删除点的父亲
            if (tempf != null) {
                // 以下的if-else语句把被删除点(temp)变成替代的点(_to)
                if (temp.getValue() < tempf.getValue()) {   
                    tempf.setLeftChild(_to);
                }
                else {
                    tempf.setRightChild(_to);
                }
            }
            if (_to != null) {
                _to.setParent(tempf);
            }
            // 以上步骤确保了temp的儿子_to已经全部转移给temp的父亲
            temp.setParent(null);   //断开temp和父亲的联系。 图像应该在这一步显示temp消失！！！！
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

        /**
         *  Compare the height of a Node's two children and get the higher number.
         *  @param  x The parent Node which contains two children to be compared.
         *  @return the max_height of a node's two children
         */
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

        public void setNewHeight(Node tempRoot) {
            tempRoot.getLeftChild().setHeight(sonMaxHeight(tempRoot.getLeftChild()) + 1);
            tempRoot.getRightChild().setHeight(sonMaxHeight(tempRoot.getRightChild()) + 1);
            tempRoot.setHeight(sonMaxHeight(tempRoot));
        }

        /**
         *  Rotate the tree to the right.
         *  @param  _root the root which will be the child
         *  @param  _povit the povit which will be the parent
         */
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

        /**
         *  Rotate the tree to the left.
         *  @param  _root the root which will be the child
         *  @param  _povit the povit which will be the parent
         */
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

        /**
         *  Renew the height of the tree after the multiplication. The will be done
         *  to all the ancestors of the node (not include itself) which is inserted 
         *  or deleted. It will also check if the tree need to be restructed. 
         *  If needed, it will be restructed through {@link restructure} method.
         *  @param  temp  the node which is needed for renewing its ancestors
         */
        public void HeightRenew(Node temp) {
            Node parent;
            if (temp == null) {
                root = null;
                return;
            }
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
            this.root = temp;
        }

        /**
         *  Restruct the Nodes based on the condition (4 cases).
         *  @param  ubNode  the unbalanced node which needs to be restructed.
         */
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

        /**
         *  Insert the value to the AVL_Tree.
         *  @param  v  the valued to be inserted.
         */
        public bool insert(int v) {
            if (root == null) {
                root = new Node(v, null);
                return true;
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
        }

        /**
         *  Search the value in the AVL_Tree. It will return if the searched node
         *  exsits or not.
         *  @param  v  the value to be deleted in AVL_Tree
         *  @return The founded Node will be returned. And return <code> null </code>
         *          if not exists.
         */
        public Node search(int v) {
            if (root == null) {
                return null;
            } else {
                Node temp = root;
                bool found = false;
                while ((temp != null) && (!found)) {
                    if (v == temp.getValue()) {
                        found = true;
                    }else if (v < temp.getValue()) {
                        temp = temp.getLeftChild();
                    }else {
                        temp = temp.getRightChild();
                    }
                }
                return temp;
            }
        }

        /**
         *  Deleted the value in the AVL_Tree. It is basically divided into
         *  three conditions.
         *  @param  v  the value which will be deleted.
         *  @return    if successfully deleted, return <code> ture </code>.
         */
        public bool delete(int v) {
            Node temp = search(v);  //temp这个点就是要删除的这个点
            if (search(v) == null) {
                return false;
            } else {
                Node tempParent = temp.getParent();
                if ((temp.getLeftChild() == null) && (temp.getRightChild() == null)) {
                    // 要删除的点左右都没孩子的情况
                    Node tempf = temp.getParent(); 
                    deleteNodeTo(temp, null); //这一步把要删除的点变成null
                    if (tempf !=null) {
                        tempf.setHeight(sonMaxHeight(tempf) + 1);  // 设置删除点父亲的高度
                    }
                    if (tempf.getLeftChild() != null) {
                        HeightRenew(tempf.getLeftChild());
                    } else if (tempf.getRightChild() != null) {
                        HeightRenew(tempf.getRightChild());
                    } else {
                        HeightRenew(tempf); //看看要不要旋转
                    }
                } else if ((temp.getLeftChild() != null) && (temp.getRightChild() == null)) {
                    // 只有左儿子
                    deleteNodeTo(temp, temp.getLeftChild());    //这一步把temp先变成他的左儿子！！！出错应该在这一步
                    HeightRenew(temp.getLeftChild());
                } else if ((temp.getLeftChild() == null) && (temp.getRightChild() != null)) {
                    deleteNodeTo(temp, temp.getRightChild());
                    HeightRenew(temp.getRightChild());
                } else {
                    Node inorderNode = temp.getRightChild();
                    while (inorderNode.getLeftChild() != null) {
                        inorderNode = inorderNode.getLeftChild();
                    }
                    int tempValue = temp.getValue();
                    temp.setValue(inorderNode.getValue());
                    if (inorderNode.getRightChild() != null) {
                        deleteNodeTo(temp, temp.getRightChild());
                        HeightRenew(temp.getRightChild());
                    } else {
                        Node tempf = inorderNode.getParent();
                        deleteNodeTo(inorderNode, null);
                        if (tempf != null) {
                            tempf.setHeight(sonMaxHeight(tempf) + 1);
                        }
                        HeightRenew(tempf);
                    }
                }
            }
            return true;
        }
    }

    public class main {
        public static void Main(string[] args)
        {
            AVL_Tree avl = new AVL_Tree();
            while (true) {
                System.Console.Write("Please input 'insert', 'search', or 'delete': ");
                string command = Console.ReadLine();
                System.Console.Write("Please input number: ");
                int newvalue = Convert.ToInt32(Console.ReadLine());
                if (command.Equals("insert")) {
                    avl.insert(newvalue);
                    avl.printAll(avl.root);
                } else if (command.Equals("search")) {
                    if (avl.search(newvalue) != null) {
                        System.Console.WriteLine("Found");
                    }else {
                        System.Console.WriteLine("Not Found");
                    }
                } else if (command.Equals("delete")) {
                    if (avl.delete(newvalue)) {
                        avl.printAll(avl.root);
                    } else {
                        System.Console.WriteLine("No Found");
                    }
                } else {
                    System.Console.WriteLine("invalid input");
                }
            }
        }
    }
}

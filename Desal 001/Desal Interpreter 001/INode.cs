using System.Collections.Generic;

//a Desal node
interface INode {
	void getInfo(out string name, out object children);
	HashSet<Identifier> identikeyDependencies { get; }
}
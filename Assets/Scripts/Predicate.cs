using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Predicate
{
	public enum Value
	{
		Six,
		Seven,
		Eight,
		Nine,
		Ten,
		Jack,
		Queen,
		King,
		Ace
	}

	public enum Mast
	{
		Clubs,
		Spades,
		Diamonds,
		Hearts,
		Red,
		Black,
		Any
	}
	bool Check(Hand hand, out List<int> hits);
}

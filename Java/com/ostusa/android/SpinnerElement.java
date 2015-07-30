package com.ostusa.android;

import java.util.ArrayList;
import java.util.List;

import com.ostusa.DynamicElement;

public class SpinnerElement extends DynamicElement
{
	List<DynamicElement> Options;
	
	public SpinnerElement()
	{
		super();
		Options = new ArrayList<DynamicElement>();
	}
	
	public void AddOptionElement(DynamicElement element)
	{
		Options.add(element);
	}
	
	public void ClickOption(String value)
	{
		for(DynamicElement currentEle : Options)
		{
			if(currentEle.getAttribute("Value") == value)
			{
				currentEle.click();
			}
		}
	}
	
	public void ClickOption(int index)
	{
		Options.get(index).click();
	}
	
	public List<DynamicElement> GetOptions()
	{
		return Options;
	}
	
}

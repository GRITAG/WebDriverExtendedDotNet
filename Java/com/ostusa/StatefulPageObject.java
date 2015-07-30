package com.ostusa;

import java.util.List;

public abstract class StatefulPageObject extends PageObject 
{
	public List<ApplicationState> States;
	
	public void AddState(String key, String value) throws InvalidStateException
	{
		for(ApplicationState currentState : States)
		{
			if(currentState.Name == key)
			{
				throw new InvalidStateException("There is all ready a states of key value: " + key + ". Key values must be unique.");
			}
		}
		
		States.add(new ApplicationState(key, value));
	}
	
	public ApplicationState GetState(String key) throws InvalidStateException
	{
		for(ApplicationState currentState: States)
		{
			if(currentState.Name == key)
			{
				return currentState;
			}
		}
		
		throw new InvalidStateException("No state was found for the key: " + key);
	}
	
	public void UpdateState(String key, String value) throws InvalidStateException
	{
		for(ApplicationState currentState : States)
		{
			if(currentState.Name == key)
			{
				currentState.Value = value;
				return;
			}
		}
		
		throw new InvalidStateException("No state was found for the key: " + key);
	}
	
	public void RemoveState(String key) throws InvalidStateException
	{
		for(int stateIndex = 0; stateIndex < States.size(); stateIndex++)
		{
			if(States.get(stateIndex).Name == key)
			{
				States.remove(stateIndex);
				return;
			}
		}
		
		throw new InvalidStateException("No state was found for the key: " + key);
	}
}

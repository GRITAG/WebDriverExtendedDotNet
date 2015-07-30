package com.ostusa;


import io.selendroid.client.SelendroidDriver;

import org.openqa.selenium.WebDriver;

public abstract class ViewObject
{
	public String ViewName;
	public WebDriver App;
	
	public abstract void Setup();
	
	public void HitBack(WebDriver app)
	{
		SelendroidDriver driver = (SelendroidDriver) app;
		driver.getKeyboard().sendKeys("\uE100");
	}
	
	public abstract void TearDown();
}

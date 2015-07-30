package com.ostusa;

import org.openqa.selenium.WebDriver;



public abstract class PageObject 
{
	public String URL;
	public WebDriver Browser;
	
	public abstract void Setup();
	
	public void Navigate(WebDriver browser, String url)
	{
		browser.navigate().to(url);
	}
	
	public void Navigate(String url)
	{
		Browser.navigate().to(url);
	}
	
	public void Navigate(WebDriver browser)
	{
		Navigate(browser, this.URL);
	}
	
	public void Navigate()
	{
		Navigate(Browser, this.URL);
	}
	
	public abstract void TearDown();
}

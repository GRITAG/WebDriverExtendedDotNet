package com.ostusa;
import java.util.List;
import org.openqa.selenium.By;
import org.openqa.selenium.Dimension;
import org.openqa.selenium.Point;
import org.openqa.selenium.WebDriver;
import org.openqa.selenium.WebDriverException;
import org.openqa.selenium.WebElement;

public class DynamicElement implements WebElement
{

	private String ID;
	private String Name;
	private String Regex;
	private String XPath;
	private String LinkText;
	
	private WebDriver Driver;
	private WebElement rootElement;
	
    public DynamicElement() { }
    
    public DynamicElement(WebDriver driver)
    {
    	this.Driver = driver;
    }
    
	public DynamicElement SetID(String id)
	{
		this.ID = id;
		return this;
	}
	
	public DynamicElement SetName(String name)
	{
		this.Name = name;
		return this;
	}
	
	public DynamicElement SetRegex(String regex)
	{
		this.Regex = regex;
		return this;
	}
	
	public DynamicElement SetXpath(String xpath)
	{
		this.XPath = xpath;
		return this;
	}
	
	public DynamicElement SetDiver(WebDriver driver)
	{
		this.Driver = driver;
		return this;
	}
	
	public DynamicElement SetLinkText(String text)
	{
		this.LinkText = text;
		return this;
	}

	private DynamicElement Find()
	{
		rootElement = null;
		
		if(ID != null)
		{
			rootElement = Driver.findElement(By.id(ID));
		}
		
		if(Name != null)
		{
			rootElement = Driver.findElement(By.name(Name));
		}
		
		if(Regex != null)
		{
			throw new WebDriverException("Regex is not yet implanted");
		}
		
		if(XPath != null)
		{
			rootElement = Driver.findElement(By.xpath(XPath));
		}
		
		if(LinkText != null)
		{
			rootElement = Driver.findElement(By.linkText(LinkText));
		}
		
		if(rootElement != null)
		{
			return this;
		}
		
		
		return null;
	}
	
	@Override
	public void clear() {
		this.Find();
		this.rootElement.clear();
		
	}

	@Override
	public void click() {
		this.Find();
		this.rootElement.click();
		
	}

	@Override
	public WebElement findElement(By arg0) {
		throw new WebDriverException("Incorrect Usage");
	}

	@Override
	public List<WebElement> findElements(By arg0) {
		throw new WebDriverException("Incorrect Usage");
	}

	@Override
	public String getAttribute(String arg0) {
		this.Find();
		return this.rootElement.getAttribute(arg0);
	}

	@Override
	public String getCssValue(String arg0) {
		this.Find();
		return this.rootElement.getCssValue(arg0);
	}

	@Override
	public Point getLocation() {
		this.Find();
		return this.rootElement.getLocation();
	}

	@Override
	public Dimension getSize() {
		this.Find();
		return this.rootElement.getSize();
	}

	@Override
	public String getTagName() {
		this.Find();
		return this.rootElement.getTagName();
	}

	@Override
	public String getText() {
		this.Find();
		return this.rootElement.getText();
	}

	@Override
	public boolean isDisplayed() {
		this.Find();
		return this.rootElement.isDisplayed();
	}

	@Override
	public boolean isEnabled() {
		this.Find();
		return this.rootElement.isEnabled();
	}

	@Override
	public boolean isSelected() {
		this.Find();
		return this.rootElement.isSelected();
	}

	@Override
	public void sendKeys(CharSequence... arg0) {
		this.Find();
		this.rootElement.sendKeys(arg0);
		
	}

	@Override
	public void submit() {
		this.Find();
		this.rootElement.submit();
		
	}
	

	
	

}

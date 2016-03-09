package com.ostusa;

/**
 * Created by vanproyaa on 3/3/2016.
 */
public abstract class Report
{
    public abstract void PrepReporting();

    public abstract void Validate(String text, boolean passFail, boolean positiveCheck, byte[] screenShot);

    public abstract void Validate(String text, boolean passFail, boolean positiveCheck);

    public abstract void Validate(String text, boolean passFail);

    public abstract void WriteStep(String text);

    public abstract void WriteReport();
}

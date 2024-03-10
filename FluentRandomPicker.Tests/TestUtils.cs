using System;
using System.Collections.Generic;
using System.Linq;
using FluentRandomPicker.FluentInterfaces.General;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluentRandomPicker.Tests;

public static class TestUtils
{
    public static void AssertAllSpecifiedValuesAndNoOthersArePossible<T>(Func<T> picking, ISet<T> possibleValues)
    {
        const int NumberOfTries = 1_000_000;
        var pickedValues = new HashSet<T>();

        for (var i = 0; i < NumberOfTries; i++)
        {
            var value = picking();
            pickedValues.Add(value);
        }

        Assert.IsFalse(possibleValues.Except(pickedValues).Any(), "Not all values can be picked.");
        Assert.IsFalse(pickedValues.Except(possibleValues).Any(), "More values han expected can be picked.");
    }

    public static void ProbabilitiesMatter<T>(this Assert assert, IPick<T> pickable,
        int tries = 1_000_000, double acceptedDeviation = 0.25, params (T value, double chance)[] valueChancesPairs)
    {
        var occurrences = new Dictionary<T, long>();

        for (var i = 0; i < tries; i++)
        {
            var value = pickable.PickOne();
            if (occurrences.ContainsKey(value))
                occurrences[value]++;
            else
                occurrences.Add(value, 1);
        }

        foreach(var (value, chance) in valueChancesPairs)
        {
            if (chance == 0 && !occurrences.ContainsKey(value))
                continue;

            Assert.IsTrue(occurrences[value] >= tries * chance * (1 - acceptedDeviation));
            Assert.IsTrue(occurrences[value] <= tries * chance * (1 + acceptedDeviation));
        }
    }

    public static void PickNProbabilitiesMatter<T>(this Assert _, IPick<T> pickable, int tries = 1_000_000,
        double acceptedDeviation = 0.25, params (T value, double chance)[] valueChancesPairs)
    {
        var occurrences = new Dictionary<T, long>(valueChancesPairs.Length);

        foreach (T value in pickable.Pick(tries))
        {
            if (occurrences.ContainsKey(value))
                occurrences[value]++;
            else
                occurrences.Add(value, 1);
        }

        foreach(var (value, chance) in valueChancesPairs)
        {
            if (chance == 0 && !occurrences.ContainsKey(value))
                continue;

            Assert.IsTrue(occurrences[value] >= tries * chance * (1 - acceptedDeviation));
            Assert.IsTrue(occurrences[value] <= tries * chance * (1 + acceptedDeviation));
        }
    }
}
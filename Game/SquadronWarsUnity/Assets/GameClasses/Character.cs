﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Character
{
    private int characterId { get; set; }
    private Stats stats { get; set; }
    private int characterListId { get; set; }
    private string name { get; set; }
    private int level { get; set; }
    private int experience { get; set; }
    private List<T> effects = new List<T>();

    public Character(int characterId, Stats stats, int characterListId, string name,
        int level, int experience)
    {
        this.characterId = characterId;
        this.stats = stats;
        this.characterListId = characterListId;
        this.name = name;
        this.level = level;
        this.experience = experience;
    }

    public void addEffect(Effect effect)
    {
        effect.execute(ref stats);
        effects.Add(effect);
    }

    public void checkEffects()
    {
        foreach (Effect effect in effects)
        {

        }
    }
}
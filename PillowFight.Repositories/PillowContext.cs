﻿using Microsoft.EntityFrameworkCore;
using PillowFight.Repositories.Models;

namespace PillowFight.Repositories
{
    public class PillowContext : DbContext
    {
        public PillowContext(DbContextOptions options) : base(options)
        { }
        public PillowContext() : base()
        { }
        public DbSet<ArmorItem> ArmorItems { set; get; }
        public DbSet<Character> Characters { set; get; }
        public DbSet<Item> Items { set; get; }
        public DbSet<Player> Players { set; get; }
        public DbSet<PlayerCharacter> PlayerCharacters { set; get; }
        public DbSet<SpellItem> SpellItems { set; get; }
        public DbSet<WeaponItem> WeaponItems { set; get; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Character>()
                .HasOne(p_c => (ArmorItem)p_c.TorsoSlotItem)
                .WithMany()
                .HasForeignKey(p_c => p_c.TorsoSlotItemId);
            modelBuilder.Entity<Character>()
                .HasOne(p_c => (WeaponItem)p_c.MainHandSlotItem)
                .WithMany()
                .HasForeignKey(p_c => p_c.MainHandSlotItemId);
            modelBuilder.Entity<Player>()
                .HasIndex(b => b.UserName)
                .IsUnique();
        }
    }
}

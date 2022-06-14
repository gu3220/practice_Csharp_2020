using System;
using System.Collections.Generic;

namespace prac
{
 class Program
 {
 public static Army army = new Army(0, 0, 0, 0);
 public static Random rand = new Random();
 public static Solder[] s = { new Solder("Barbarian", 152, 15, "Leather Armor", 20) };
 public static Animal[] a = { new Animal("Elephant", 101, 50, 5) };
 public static Mechanism[] m = { new Mechanism("Catapult", 725, 30, "Bullet", "Sonic Draconic Bite", rand.Next(3, 7)),
 new Mechanism ("Ballista", 496, 29, "Bullet", "Astral Sonic Blast", rand.Next(3, 7)),
 new Mechanism("Catapult", 610, 18, "Bolt", "Astral Sonic Blast", 
rand.Next(3, 7)) };
 public static List<Unit> u = new List<Unit>(s.Length+a.Length+m.Length);
 public static void get_unit()
 {
 for(int i=0; i<s.Length; i++)
 {
 u.Add(new Unit( s[i].name, s[i].health, s[i].damage ));
 }
 for (int i = 0; i < a.Length; i++)
 {
 u.Add(new Unit(a[i].name, a[i].health, a[i].damage));
 }
 for (int i = 0; i < m.Length; i++)
 {
 u.Add(new Unit(m[i].name, m[i].health, m[i].damage*m[i].ammo_volume));
 }
 }
 static void Main(string[] args)
 {
 Console.ResetColor();
 get_unit();
 get_Army();
 stats();
 Console.WriteLine("\n");
 army.tell();
 
Console.WriteLine("\n____________________________________________________________________\n");
 Console.ForegroundColor = ConsoleColor.DarkYellow;
 Console.WriteLine("\t\tPRESS ANY BUTTON TO START THE GAME"); 
 Console.ReadKey();
 Console.Clear();
 Console.WriteLine("\t\tGAME STRTS");
 Console.ResetColor();
 army.tell();
 while(army.common_population != 0)
 {
 
Console.WriteLine("\n____________________________________________________________________\n");
 Console.ForegroundColor = ConsoleColor.DarkYellow;
 Console.WriteLine("\n\tPRESS A TO ATTACK\tor\tPRESS S TO GET UNIT STATISTICS");
 Console.ResetColor();
 
Console.WriteLine("\n____________________________________________________________________\n");
 Console.Write("YOUR CHOICE: ");
 string answ = Console.ReadLine();
 switch (answ)
 {
 case "A":
 Attack();
break;
 case "S":
 stats();
break;
 default:
 Console.ForegroundColor = ConsoleColor.DarkRed;
Console.WriteLine("\t\tYOU LEFT THE GAME");
 Console.ResetColor();
return; 
 }
 }
 Console.ForegroundColor = ConsoleColor.DarkRed;
 if (army.common_population == 0) Console.WriteLine("\t\tGAME OVER");
 Console.ResetColor();
 }
 public static void stats()
 {
 for(int i=0; i<s.Length; i++)
 {
 s[i].health = u[i].health;
 if (s[i].health == 0) s[i].damage = 0;
 s[i].tell();
 }
 for(int j=s.Length; j<s.Length+a.Length; j++)
 {
 a[j-s.Length].health = u[j].health;
 if (a[j - s.Length].health == 0)
 { 
 a[j - s.Length].damage = 0;
 a[j - s.Length].speed = 0;
 }
 a[j - s.Length].tell();
 }
 int length = 0;
 if (s.Length == a.Length || a.Length < s.Length) length = a.Length;
 if (s.Length < a.Length) length = s.Length;
 for(int i=0; i<length; i++)
 {
 if (s[i].health != 0)
 {
 Console.ForegroundColor = ConsoleColor.DarkGreen;
Console.Write($"\n\n\tClass: Rider = {s[i].name} + {a[i].name}");
 Console.ResetColor();
Console.WriteLine($"\n\tHealth: {s[i].health + a[i].health}\n\tDamage: {s[i].damage + a[i].damage}"); 
 }
 }
 for (int k = s.Length + a.Length; k < s.Length + a.Length + m.Length; k++)
 {
 m[k - s.Length - a.Length].health = u[k].health;
 if (m[k - s.Length - a.Length].health == 0) m[k - s.Length-a.Length].damage=0;
 m[k - s.Length - a.Length].tell();
 }
 }
 public static void Attack()
 {
 int attack = rand.Next(30, 200);
 Console.ForegroundColor = ConsoleColor.DarkRed;
 Console.WriteLine($"\n---------Attack: {attack}");
 Console.ResetColor();
 while (attack > 0 & army.common_health != 0)
 {
 if(army.common_protection != 0)
 {
 for (int i = 0; i < s.Length; i++)
 {
 if (s[i].extra_protection < attack || s[i].extra_protection == attack)
{
 attack -= s[i].extra_protection;
s[i].extra_protection = 0;
 get_Army();
 }
else
{
 s[i].extra_protection -= attack;
attack = 0;
get_Army();
 }
 }
 }
 for(int i=0; i<u.Count; i++)
 {
 if(u[i].health != 0)
{
 if (u[i].health < attack || u[i].health == attack)
 {
 attack -= u[i].health;
u[i].health = 0;
get_Army();
 }
else
{
 u[i].health -= attack;
attack = 0;
get_Army();
 }
 }
 }
 }
 army.tell();
 }
static public void get_Army()
 {
 army.common_health =0;
 army.common_damage =0;
 army.common_population =0;
 foreach (Unit unit in u)
 {
 army.common_health += unit.health; 
 if (unit.health != 0)
 {
 army.common_population += 1;
army.common_damage += unit.damage;
 }
 }
 for(int i=0;i<s.Length; i++)
 {
 army.common_protection = s[i].extra_protection;
 } 
 } 
 }
 class Army
 {
 public int common_damage;
 public int common_protection;
 public int common_health;
 public uint common_population;
 public Army(int common_damage, int common_protection, int common_health, uint
common_population)
 {
 this.common_damage = common_damage;
 this.common_protection = common_protection;
 this.common_health = common_health;
 this.common_population = common_population;
 }
 public void tell()
 {
 Console.ForegroundColor = ConsoleColor.DarkGreen;
 Console.WriteLine("\n\tArmy");
 Console.ResetColor();
 Console.WriteLine($"\tDamage: {common_damage}\n\tProtection: {common_protection}\n\tHealth: {common_health}\n\tPopulation: {common_population}");
 }
 }
 class Unit
 {
 public string name;
 public int health;
 public int damage;
 public Unit(string name, int health, int damage)
 {
 this.name = name;
 this.health = health;
 this.damage = damage;
 }
 public virtual void tell()
 {
 Console.ForegroundColor = ConsoleColor.DarkGreen;
 Console.Write($"\n\n\tClass: {name}");
 Console.ResetColor();
 Console.WriteLine($"\n\tHealth: {health}\n\tDamage: {damage}");
 }
 }
 class Mechanism : Unit
 {
 public string ammo_type;
 public string ammo_name;
 public int ammo_volume;
 public Mechanism(string name, int health, int damage, string ammo_type, string
ammo_name, int ammo_volume) : base(name, health, damage)
 {
 this.ammo_type = ammo_type;
 this.ammo_name = ammo_name;
 this.ammo_volume = ammo_volume;
 }
 public override void tell()
 {
 base.tell();
 {
 Console.WriteLine($"\tAmmo type: {ammo_type}\n\tAmmo name: {ammo_name}\n\tAmmo volume: {ammo_volume}\n\tAll damage: {ammo_volume*damage}");
 }
 }
 }
 class Solder : Unit
 {
 public string armor;
 public int extra_protection;
 public Solder( string name, int health, int damage, string armor, int
extra_protection) : base(name, health, damage)
 {
 this.armor = armor;
 this.extra_protection = extra_protection;
 }
 public override void tell()
 {
 base.tell();
 {
 Console.WriteLine($"\tArmor: {armor}\n\tExtra protection: {extra_protection}");
 }
 }
 }
 class Animal : Unit
 {
 public int speed;
 public Animal (string name, int health, int damage, int speed) : base(name, health, 
damage)
 {
 this.speed = speed;
 }
 public override void tell()
 {
 base.tell();
 {
 Console.WriteLine($"\tSpeed: {speed}");
 }
 }
 }
}
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using MvcEfCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcEfCore.Data
{
    public class DummyData
    {
        public static void Initialize(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();
                context.Database.EnsureCreated();
                //context.Database.Migrate();

                // Look for any teams.
                if (context.Teams.Any())
                {
                    return;   // DB has already been seeded
                }

                var teams = DummyData.GetTeams().ToArray();
                context.Teams.AddRange(teams);
                context.SaveChanges();

                var players = DummyData.GetPlayers(context).ToArray();
                context.Players.AddRange(players);
                context.SaveChanges();
            }
        }

        public static List<Team> GetTeams()
        {
            List<Team> teams = new List<Team>() {
            new Team() {
                TeamName="Canucks",
                City="Vancouver",
                Province="BC",
                Country="Canada",
            },
            new Team() {
                TeamName="Sharks",
                City="San Jose",
                Province="CA",
                Country="USA",
            },
            new Team() {
                TeamName="Oilers",
                City="Edmonton",
                Province="AB",
                Country="Canada",
            },
            new Team() {
                TeamName="Flames",
                City="Calgary",
                Province="AB",
                Country="Canada",
            },
            new Team() {
                TeamName="Leafs",
                City="Toronto",
                Province="ON",
                Country="Canada",
            },
            new Team() {
                TeamName="Ducks",
                City="Anaheim",
                Province="CA",
                Country="USA",
            },
            new Team() {
                TeamName="Lightening",
                City="Tampa Bay",
                Province="FL",
                Country="USA",
            },
            new Team() {
                TeamName="Blackhawks",
                City="Chicago",
                Province="IL",
                Country="USA",
            },
        };

            return teams;
        }

        public static List<Player> GetPlayers(ApplicationDbContext context)
        {
            List<Player> players = new List<Player>() {
            new Player {
                FirstName = "Sven",
                LastName = "Baertschi",
                TeamName = context.Teams.Find("Canucks").TeamName,
                Position = "Forward"
            },
            new Player {
                FirstName = "Hendrik",
                LastName = "Sedin",
                TeamName = context.Teams.Find("Canucks").TeamName,
                Position = "Left Wing"
            },
            new Player {
                FirstName = "John",
                LastName = "Rooster",
                TeamName = context.Teams.Find("Flames").TeamName,
                Position = "Right Wing"
            },
            new Player {
                FirstName = "Bob",
                LastName = "Plumber",
                TeamName = context.Teams.Find("Oilers").TeamName,
                Position = "Defense"
            },
        };

            return players;
        }
    }
}

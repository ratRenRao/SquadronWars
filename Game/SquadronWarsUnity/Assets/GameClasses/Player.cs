using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Player
{
    private string username { get; set; } // remove and create LoginChange class for changes
    private string password { get; set; } // remove and create LoginChange class for changes
    private string firstName { get; set; }
    private string lastName { get; set; }
    private string email { get; set; }
    private DateTime? lastLogin { get; set; }
    private int squadId { get; set; }
    private int characterListId { get; set; }
    private int itemListId { get; set; }
    private int squadListId { get; set; }

    public Player(string username, string password, string firstName, string lastName, string email,
        DateTime? lastLogin, int characterListId, int itemListId, int squadListId)
    {
        this.username = username;
        this.password = password;
        this.firstName = firstName;
        this.lastName = lastName;
        this.email = email;
        this.lastLogin = lastLogin;
        this.characterListId = characterListId;
        this.itemListId = itemListId;
        this.squadListId = squadListId;
    }
}

# ManzelOS 🏡

✅ Simplify property management with an all-in-one platform to track tenants, properties, workers, and rent payments—built to reduce stress and save time.

## Motivation 🔥

After managing rental properties for some time, I discovered that the most critical aspect of running a real estate business is staying up-to-date with your current operations. At the start turned to Excel spreadsheets to track my rental units, but I quickly realized this approach was unsustainable—it didn't scale, became messy fast, and required significant overhead to maintain.

That's when ManzelOS was born: a purpose-built solution to make managing rental properties easier, more efficient, and actually scalable.

## Quick Start 🚀

### Prerequisites

- [SQL Server Management Studio (SSMS)](https://learn.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms)
- [.NET SDK](https://dotnet.microsoft.com/download) (version 8.0 or higher)
- [Visual Studio](https://visualstudio.microsoft.com/) or your preferred IDE
- ADO.NET (included with .NET SDK)

### 1. Clone the Repository

git clone <https://github.com/AbdulrahmanBasabrain/ManzelOS-1.0.git>
cd ManzelOS-1.0

### 2. Set Up the Database

1. **Create a new database** in SQL Server Management Studio
2. **Locate the database script:**
   - Navigate to `ManzelOS-data-access-layer/` in the project directory
   - Find the file `create_manzelOs_database.sql`
3. **Execute the script:**
   - Open the script in SSMS
   - Run it against your newly created database
4. **Configure connection settings:**
   - Open `ManzelOS-data-access-layer/clsDataAccessSettings.cs`
   - Update the file with your SQL Server credentials

Server=YOUR_SERVER_NAME;Database=ManzelOS;User Id=YOUR_USERNAME;Password=YOUR_PASSWORD;

**Note:** Database configuration will be streamlined in future releases. The current setup is designed for demonstration purposes.

Check out the [TODO.md](./TODO.md) for upcoming features and known issues.

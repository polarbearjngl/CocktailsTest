## CocktailsTest
Solution with Test Cases for [Cocktail Exercise](https://github.com/Terri14/Cocktail) written according to BDT approach. Based on Specflow. 

### How execute tests and get report
* Clone from github
```markdown 
git clone https://github.com/polarbearjngl/CocktailsTest.git
```
* Build project from Visual Studio or via CLI
```markdown 
cd Cocktails\CocktailsTests
msbuild
```
* Start tests via Test Explorer in Visual Studio or via CLI
```markdown 
dotnet test CocktailsTests.csproj
```
* Build Allure Report after tests. If you do not have Allure installed see [Manual installation](https://docs.qameta.io/allure/#_installing_a_commandline) or [Installation from npm](https://www.npmjs.com/package/allure-commandline) 
```markdown 
cd PathToDirWhere `allure-results` directoryIsPlaced
allure serve ./allure-results/
```
* Check pre-generated [allure-report](allure-report-example.html) of Cocktails Test. Need to open in Browser after cloning the repo

### Tests Cases
#### Ingredients search
###### Search By Ingredients Name

| step | description               | request                 | expected result                                                  |
|------|---------------------------|-------------------------|------------------------------------------------------------------|
| 1    | Get ingredients by Name   | /search.php?i=**Vodka** | status code 200                                                  |
| 2    | Check Response            |                         | is not empty and contains 1 item                                 |
| 3    | Validate Response Content |                         | response content for **Vodka** is valid for Alcoholic Ingredient |

Repeat with **Whiskey** (Alcoholic), **Water** (NonAlcoholic)

###### Search By Not Existed Ingredients Name

| step | description               | request                    | expected result   |
|------|---------------------------|----------------------------|-------------------|
| 1    | Get ingredients by Name   | /search.php?i=**NukaKola** | status code 200   |
| 2    | Check Response            |                            | is empty          |

Repeat with **Kvas**

###### Validate Ingredient Data

| step | description                  | request                 | expected result                                                       |
|------|------------------------------|-------------------------|-----------------------------------------------------------------------|
| 1    | Get ingredients by Name      | /search.php?i=**Vodka** | status code 200                                                       |
| 2    | Check Response               |                         | is not empty and contains 1 item                                      |
| 3    | Validate Response Content    |                         | response content for **Vodka** is valid for Alcoholic Ingredient      |
| 4    | Validate Fields from Content |                         | Fields from Content is equal to expected, based on type of Ingredient |

Repeat with **Whiskey** (Alcoholic), **Water** (NonAlcoholic)

#### Cocktails Search
###### Search By Cocktails Name

| step | description               | request                              | expected result                    |
|------|---------------------------|--------------------------------------|------------------------------------|
| 1    | Get Cocktails by Name     | /search.php?s=**Espresso%Martini**   | status code 200                    |
| 2    | Check Response            |                                      | is not empty and contains 1 item   |

Repeat with **White%Russian**, **Smashed%Watermelon%Margarita**

###### Search By Not Existed Cocktails Name

| step | description                | request                            | expected result    |
|------|----------------------------|------------------------------------|--------------------|
| 1    | Get Cocktails by Name      | /search.php?s=**Green%Russian**    | status code 200    |
| 2    | Check Response             |                                    | is empty           |

Repeat with **Nuka%Kola**

###### Search By Cocktails Name is case-insensitive

| step | description                             | request                              | expected result                  |
|------|-----------------------------------------|--------------------------------------|----------------------------------|
| 1    | Get Cocktails by Name                   | /search.php?s=**Espresso%Martini**   | status code 200                  |
| 2    | Check Response                          |                                      | is not empty and contains 1 item |
| 3    | Get Cocktails by Name                   | /search.php?s=**espresso%martini**   | status code 200                  |
| 4    | Check Response                          |                                      | is not empty and contains 1 item |
| 5    | Get Cocktails by Name                   | /search.php?s=**ESPRESSO%MARTINI**   | status code 200                  |
| 6    | Check Response                          |                                      | is not empty and contains 1 item |
| 7    | Check that all 3 contents are identical |                                      | content is identical             |

###### Search By Cocktails Name and validate Schema

| step | description              | request                              | expected result                                             |
|------|--------------------------|--------------------------------------|-------------------------------------------------------------|
| 1    | Get Cocktails by Name    | /search.php?s=**Espresso%Martini**   | status code 200                                             |
| 2    | Check Response           |                                      | is not empty and contains 1 item                            |
| 3    | Validate response schema |                                      | json schema is valid (see JsonSchemas dir for references)   |

### Non-functional checks
###### Performance Testing
1) generate amount of requests to API that equal to accessing of 100 (500, 1000, etc.) users at once
2) response time from API should be not greater than 1ms (5ms, 10ms, etc.)
3) system should be not overloaded due to amount of new logs on server, read-write operations

###### Denial of Service (DoS) attacks Testing
1) generate big amount of requests (e.g. 100, 1000, 10000 in second/minute) to API from one single user
2) system should block (ignore) requests from this particular user. Those requests should not affect accessibility to services
3) requests from other users (that are not making bursts requests) is not blocked
4) after some period of time block is removed for user from step 1

It can be automated using
* Apache JMeter
* LoadRunner

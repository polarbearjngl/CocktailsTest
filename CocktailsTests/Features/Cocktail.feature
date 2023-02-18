@API @Cocktails @CocktailSearch
Feature: Cocktail Search

Scenario: Search By Cocktails Name
	When I Get Cocktails by Name '<Name>' and saving the 'response'
	Then the status code of the 'response' is '200'
		And the Cocktail 'response' is not empty and contains '1' item

	Examples: 
    | Name							|
    | Espresso%Martini				|
	| White%Russian					|
    | Smashed%Watermelon%Margarita  |

Scenario: Search By not existed Cocktails Name
	When I Get Cocktails by Name '<Name>' and saving the 'response'
	Then the status code of the 'response' is '200'
		And the Cocktail 'response' is empty
	
	Examples: 
    | Name            |
    | Nuka%Kola       |
	| Green%Russian   |

Scenario: Search By Cocktails Name is case-insensitive
	When I Get Cocktails by Name '<OriginName>' and saving the 'responseOrigin'
	Then the status code of the 'responseOrigin' is '200'
		And the Cocktail 'responseOrigin' is not empty and contains '1' item
	When I Get Cocktails by Name '<LowerName>' and saving the 'responseLower'
	Then the status code of the 'responseLower' is '200'
		And the Cocktail 'responseLower' is not empty and contains '1' item
	When I Get Cocktails by Name '<UpperName>' and saving the 'responseUpper'
	Then the status code of the 'responseUpper' is '200'
		And the Cocktail 'responseUpper' is not empty and contains '1' item
	And 'responseOrigin,responseLower,responseUpper' are same

	
	Examples: 
    | OriginName					| LowerName					    | UpperName					    |
    | Espresso%Martini				| espresso%martini				| ESPRESSO%MARTINI				|
	| White%Russian					| white%russian					| WHITE%RUSSIAN					|
    | Smashed%Watermelon%Margarita  | smashed%watermelon%margarita  | SMASHED%WATERMELON%MARGARITA  |

Scenario: Search By Cocktails Name and validate Schema
    When I Get Cocktails by Name '<Name>' and saving the 'response'
    Then the status code of the 'response' is '200'
        And the 'response' matches json schema 'DrinksResponse'

	Examples: 
    | Name							|
    | Espresso%Martini				|
	| Coffee     					|
    | Smashed%Watermelon%Margarita  |

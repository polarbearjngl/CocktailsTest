@API @Cocktails @IngredientSearch
Feature: Ingredient Search

Scenario: Search By Ingredients Name
	When I Get ingredients by Name '<Name>' and saving the 'response'
	Then the status code of the 'response' is '200'
		And the Ingredient 'response' is not empty and contains '1' item
		And the '<Alco>' Ingredient 'response' content for '<Name>' is valid
	
	Examples: 
    | Name    | Alco         |
    | Vodka   | Alcoholic    |
	| Whiskey | Alcoholic    |
    | Water   | NonAlcoholic |

Scenario: Validate Ingredient Data
	When I Get ingredients by Name '<Name>' and saving the 'response'
	Then the status code of the 'response' is '200'
		And the Ingredient 'response' is not empty and contains '1' item
		And the '<Alco>' Ingredient 'response' content for '<Name>' is valid
		And the data from 'response' for '<Name>' saved into 'Object' for validation
		And the Ingredient 'Object' equal to expected:
		| IngredientId    | IngredientName | Type    | Alcohol    | ABV   |
		| <IngredientId>  | <Name>         | <Type>  | <Alcohol>  | <ABV> |

	Examples: 
    | Name    | Alco          | IngredientId | IngredientName | Type    | Alcohol | ABV     |
    | Vodka   | Alcoholic     | 1            | Vodka          | Vodka   | Yes     | 40      |
    | Whiskey | Alcoholic     | 600          | Whiskey        | Whisky  | Yes     | 40      |
	| Water   | NonAlcoholic  | 513          | Water          | Water   | No      | <null>  |

Scenario: Search By not existed Ingredients Name
	When I Get ingredients by Name '<Name>' and saving the 'response'
	Then the status code of the 'response' is '200'
		And the Ingredient 'response' is empty

	Examples: 
    | Name     |
    | NukaKola |
	| Kvas     |

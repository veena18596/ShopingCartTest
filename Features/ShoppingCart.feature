Feature: ShoppingCart

A short summary of the feature

Scenario: Remove the lowest price item from the cart
    Given I have added four random items to my cart
    When I view my cart
    And I search for the lowest price item
    And I remove the lowest price item from my cart
    Then I should verify that three items are left in my cart
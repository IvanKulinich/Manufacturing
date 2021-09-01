# .NET Software Engineer Technical Assignment (Manufacturing)

One of the microservices in the ecosystem is the API with operations to place and retrieve orders.

## Business flow

A customer can order 1 or multiple products (e.g. photo book, 2 calendars, 2 mugs).
After the order is produced, its contents (products) are put into the package and shipped. Package is delivered to the pickup point, and stored in a bin on a shelf, waiting to be picked up by a customer. The bin has to be sufficiently wide to fit the package. Bins are reserved upfront, so we need to calculate the minimum bin width at the order creation.
Package width is calculated based on the product type and quantity. See Simplified width calculation rules for more details.

The API consists of 2 endpoints:

1. `/orders/place`
Receives a list of order lines and adds a new order into the data store.
Also, the package width is calculated and saved at the order.


2. `order/{id}/retrieve`
Receives an id, and returns the order.

## Simplified width calculation rules

Products are put in the package next to each other.
For example, 1 photo book (`l`), 2 calendars (`|`) and 1 mug (`.`) should look like this in the package:

 `l||.`

But a mug has 1 detail: it can be stacked onto each other (up to 4 in a stack). So, an order with 1 photo book, 2 calendars and 2 mugs should be positioned like this:

 `l||:`

So the width of the 2 packages above is exactly the same. It would be still the same even if we add 2 more mugs (4 in total), but would increase when we add a 5th mug.
Package width occupied by 1 product of the type:
* 1 photo book consumes 19 mm of package width
* 1 calendar: 10 mm of package width.
* 1 canvas: 16 mm
* 1 cards: 4.7 mm
* 1 mug: 94 mm

## Things we look for in this test

* We would like you to show us your skills in building testable and maintainable software with design and architecture in mind using industry best practices.
* We value “simple” more highly than “complex”, “working” is better than “nice”. Readability matters.
* This is an initial version of the application. It is already functional, but there are some problems in the code, and some bad practices. They are opportunities for refactoring, design improvements, standards and readability.
* We are looking for your contributions, so feel free to improve what you think is important, in terms of design, good practices, quality, and so on.
* Please don't spend too much time on this: focus on the major issues first, and finish as much you can in about 2-4 hours.

## Important remarks

- The special stacking logic for Mugs is not in place yet, please implement it in order to calculate the correct package width. There is currently one failing test for this reason.

- The repositories contain some hardcoded data, instead of communicating to an external source. To keep the test simple, without external dependencies, you can keep this as is.

- To send the code to us, please fork this repository to your github account, commit your changes to a branch (readability matters), and create pull request.

- Along with the code improvements, it would be nice if you provide a list of improvements or problems you have found, and if applicable, some comments on why the refactoring was made.

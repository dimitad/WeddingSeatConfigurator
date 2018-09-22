# WeddingSeatConfigurator

Library that helps assign seating to wedding guests given a list of available tables and guest reservations.

## Getting Started

The console application used to run the seating plan is called WeddingSeating. It expects a text file as input in the following format:
- tables: table_letter_id-number of seats
- Name, party of number_of_guests dislikes Names(comma separated)
  
  Example: 
  tables: A-8 B-8 C-7 D-7
  Smith, party of 10 
  Jones, party of 7 dislikes Smith

### Installation

Open with Visual Studio 2017 and build the solution. Run WeddingSeating.exe from the Debug folder of the WeddingSeating project passing a text file per the above requirements as a parameter. 

## Running the tests

Unit tests require NUnit 3.10.0

## License

This project is licensed under the Apache License 2.0 License - see the [LICENSE.md](LICENSE.md) file for details

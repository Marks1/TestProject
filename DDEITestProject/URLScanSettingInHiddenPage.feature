Feature: URL Scan Setting in Hidden Page
	In order to reduce mail delay caused by URL scan
	As a DDEI admin
	I want to diable URL scan fucntion in hidden page

@DisableURLscan
Scenario: Disable URL scan function in hidden page
	Given I have login DDEI hidden page
	And I disable the function of URL scanning
	When DDEI process mail with malicious URL
	Then The URL will not be scanned
	

@EnableURLscan
Scenario: Enable URL scan function in hidden page
	Given I have login DDEI hidden page
	And I enable the function of URL scanning
	When DDEI process mail with malicious URL
	Then The URL will be scanned


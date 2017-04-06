Steps to follow:

1. Click Image Start and in the instance search type "Local Security Policy" and 
        as it appears in the search result, press Enter to run it, note: UAC will prompt.
        So pass the UAC prompt correctly.  
        (Note: You can also press Image + R and when the Run dialog appears type: "secpol.msc" and press Enter). 
2. Now expand the Local Policies tree, and click on the Security Options. 
3. On the right-side a list of security settings will appear, search for "User Account Control: Behavior of the elevation prompt for administrators in Admin Approval Mode". 
4. Right-click on it and choose Properties a dialog will appear. 
5. In the middle of the dialog, you'll see a comboBox with the default value = "Prompt for consent for non-Windows binaries". 
6. Click on that comboBox and choose "Elevate without prompting", click Apply and OK 
        (Note: This does NOT require a PC reboot).
7. Double click on the program to verify if it works, but, trust me this will work. 

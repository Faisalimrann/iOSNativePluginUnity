//
//  SwiftPlugin.swift
//  medialib
//
//  Created by Mac on 27/08/2020.
//  Copyright © 2020 Mac. All rights reserved.
//

import Foundation
import UIKit
import MessageUI
@objc public class SwiftPlugin: UIViewController {
      @objc public static let shared = SwiftPlugin()
      @objc public func SayHiToUnity() -> String{
            return "Hi, I'm Swift"
      }
    @objc public func sendEmail() {
        // Modify following variables with your text / recipient
        let recipientEmail = "test@email.com"
        let subject = "Multi client email support"
        let body = "This code supports sending email via multiple different email apps on iOS! :)"

        // Show default mail composer
        if MFMailComposeViewController.canSendMail() {
            let mail = MFMailComposeViewController()
            //mail.mailComposeDelegate = self
            mail.setToRecipients([recipientEmail])
            mail.setSubject(subject)
            mail.setMessageBody(body, isHTML: false)

            present(mail, animated: true)

        // Show third party email composer if default Mail app is not present
        }
    }

}
